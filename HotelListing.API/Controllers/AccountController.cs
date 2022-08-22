using HotelListing.API.Contracts;
using HotelListing.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthManager authMgr;
    public AccountController(IAuthManager authManager)
    {
        authMgr = authManager;
    }

    // api/Account/register
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] APIUserDTO usrDTO)
    {
        var errors = await authMgr.Register(usrDTO);
        if(errors.Any())
        {
            foreach(var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return Ok(usrDTO);
    }

    // api/Account/login
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginUserDTO loginDTO)
    {
        var authResponse = await authMgr.Login(loginDTO);
        if (authResponse == null) return Unauthorized();

        return Ok(authResponse);
    }
    // api/Account/refreshtoken
    [HttpPost]
    [Route("refreshtoken")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RefreshToken([FromBody] AuthResponseDTO request)
    {
        var authResponse = await authMgr.VerifyRefreshToken(request);
        if (authResponse == null) return Unauthorized();

        return Ok(authResponse);
    }
}
