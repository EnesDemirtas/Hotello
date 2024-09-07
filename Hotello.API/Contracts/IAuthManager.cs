using Hotello.API.Models.User;
using Microsoft.AspNetCore.Identity;

namespace Hotello.API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(ApiUserDTO userDTO);
    Task<bool> Login(LoginDTO loginDTO);
}
