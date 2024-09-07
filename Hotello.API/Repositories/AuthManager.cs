using AutoMapper;
using Hotello.API.Contracts;
using Hotello.API.Data;
using Hotello.API.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Hotello.API.Repositories;

public class AuthManager : IAuthManager
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApiUser> _userManager;
    public AuthManager(IMapper mapper, UserManager<ApiUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO)
    {
        var user = _mapper.Map<ApiUser>(userDTO);
        user.UserName = userDTO.Email;

        var result = await _userManager.CreateAsync(user, userDTO.Password);

        if (result.Succeeded)
            await _userManager.AddToRoleAsync(user, "User");

        return result.Errors;
    }
}
