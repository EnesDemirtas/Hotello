﻿using Hotello.Core.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Hotello.Core.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO);
    Task<AuthResponseDTO> Login(LoginDTO loginDTO);
    Task<string> CreateRefreshToken();
    Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request);
}
