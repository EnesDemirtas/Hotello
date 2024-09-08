using Hotello.Core.Contracts;
using Hotello.Core.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Hotello.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly ILogger<AccountController> _logger;
    public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
    {
        _authManager = authManager;
        _logger = logger;
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] ApiUserDTO userDTO)
    {
        _logger.LogInformation($"Registration attempt for {userDTO.Email}");
        var errors = await _authManager.Register(userDTO);

        if (errors.Any())
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return Ok();
    }

    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        _logger.LogInformation($"Login attempt for {loginDTO.Email}");

        var authResponse = await _authManager.Login(loginDTO);

        if (authResponse == null)
        {
            return Unauthorized();
        }

        return Ok(authResponse);


    }

    [HttpPost]
    [Route("refreshtoken")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDTO request)
    {
        var authResponse = await _authManager.VerifyRefreshToken(request);

        if (authResponse == null)
        {
            return Unauthorized();
        }

        return Ok(authResponse);
    }
}
