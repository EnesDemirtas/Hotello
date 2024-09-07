using Hotello.API.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Hotello.API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO);
    Task<AuthResponseDTO> Login(LoginDTO loginDTO);
    Task<string> CreateRefreshToken();
    Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request);
}
