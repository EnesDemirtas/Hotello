﻿using AutoMapper;
using Hotello.API.Contracts;
using Hotello.API.Data;
using Hotello.API.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hotello.API.Repositories;

public class AuthManager : IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    private readonly IConfiguration _configuration;
    private ApiUser _user;

    private const string _loginProvider = "HotelloAPI";
    private const string _refreshToken = "RefreshToken";
    public AuthManager(
        IMapper mapper,
        UserManager<ApiUser> userManager,
        IConfiguration configuration)
    {
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> CreateRefreshToken()
    {
        await _userManager.RemoveAuthenticationTokenAsync(_user, _loginProvider, _refreshToken);
        var newRefreshToken = await _userManager.GenerateUserTokenAsync(_user, _loginProvider, _refreshToken);
        var result = await _userManager.SetAuthenticationTokenAsync(_user, _loginProvider, _refreshToken, newRefreshToken);

        return newRefreshToken;
    }

    public async Task<AuthResponseDTO> Login(LoginDTO loginDTO)
    {
        _user = await _userManager.FindByEmailAsync(loginDTO.Email);
        bool isValidUser = await _userManager.CheckPasswordAsync(_user, loginDTO.Password);

        if (_user == null || isValidUser == false)
            return null;

        var token = await GenerateToken();

        return new AuthResponseDTO
        {
            Token = token,
            UserId = _user.Id
        };
    }

    public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO)
    {
        var _user = _mapper.Map<ApiUser>(userDTO);
        _user.UserName = userDTO.Email;

        var result = await _userManager.CreateAsync(_user, userDTO.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(_user, "User");

        return result.Errors;
    }

    public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(request.Token);
        var username = tokenContent.Claims.ToList().FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)?.Value;
        _user = await _userManager.FindByNameAsync(username);

        if (_user == null || _user.Id != request.UserId)
            return null;

        var isValidRefreshToken = await _userManager.VerifyUserTokenAsync(_user, _loginProvider, _refreshToken, request.Token);

        if (isValidRefreshToken)
        {
            var token = await GenerateToken();
            return new AuthResponseDTO
            {
                UserId = _user.Id,
                Token = token,
                RefreshToken = await CreateRefreshToken()
            };
        }

        await _userManager.UpdateSecurityStampAsync(_user);
        return null;
    }

    private async Task<string> GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(_user);
        var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(_user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, _user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, _user.Email),
            new Claim("uid", _user.Id)
        }
        .Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
