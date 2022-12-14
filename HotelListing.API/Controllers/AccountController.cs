using HotelListing.API.Core.Contracts;
using HotelListing.API.Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace HotelListing.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthManager authMgr;
    private readonly ILogger<AccountController> lggr;

    public AccountController(IAuthManager authManager, ILogger<AccountController> logger)
    {
        authMgr = authManager;
        lggr = logger;
    }

    // api/account/register
    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Register([FromBody] APIUserDTO usrDTO)
    {
        var errors = await authMgr.Register(usrDTO);
        if (errors.Any())
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }

        return Ok(usrDTO);
    }

    // api/account/login
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> Login([FromBody] LoginUserDTO loginDTO)
    {
        lggr.LogInformation($"Login attempty by {loginDTO.UserName}");
        var authResponse = await authMgr.Login(loginDTO);
        if (authResponse == null) return Unauthorized();

        return Ok(authResponse);
    }
    
    // api/account/refreshtoken
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

    // api/account/promote
    [HttpPatch]
    [Route("promote")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Promote(string userName)
    {
        var errors = await authMgr.PromoteToAdmin(userName);

        if (errors == null) return NotFound();
        else
        {
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
    }

    // api/account/demote
    // Diff between PUT y PATCH https://www.bbvanexttechnologies.com/blogs/como-utilizar-los-metodos-put-y-patch-en-el-diseno-de-tus-apis-restful/
    [HttpPatch]
    [Route("demote")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Authorize(Roles = "Administrator")]
    public async Task<ActionResult> Demote(string userName)
    {
        var errors = await authMgr.DemoteToUser(userName);

        if (errors == null) return NotFound();
        else
        {
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
    }
}
