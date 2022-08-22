using AutoMapper;
using HotelListing.API.Contracts;
using HotelListing.API.Data.Models;
using HotelListing.API.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelListing.API.Repository;

public class AuthManager : IAuthManager
{
    private readonly IMapper mppr;
    private readonly UserManager<APIUser> usrMgr;
    private readonly IConfiguration cfg;
    private APIUser user;

    private const string LOGIN_PROVIDER = "HotelListingApi";
    private const string REFRESHTOKEN = "RefreshToken";

    public AuthManager(IMapper mapper, UserManager<APIUser> userManager, IConfiguration configuration)
    {
        mppr = mapper;
        usrMgr = userManager;
        cfg = configuration;
    }

    public async Task<AuthResponseDTO> Login(LoginUserDTO loginDTO)
    {
        user = await usrMgr.FindByNameAsync(loginDTO.UserName);
        bool isValidUser = await usrMgr.CheckPasswordAsync(user, loginDTO.Password);

        if (user == null || isValidUser == false) return null;

        var token = await GenerateToken();
        return new AuthResponseDTO
        {
            Token = token,
            UserId = user.Id,
            RefreshToken = await CreateRefreshToken()

        };
    }

    public async Task<IEnumerable<IdentityError>> Register(APIUserDTO apiUserDTO)
    {
        user = mppr.Map<APIUser>(apiUserDTO);

        var result = await usrMgr.CreateAsync(user, apiUserDTO.Password);

        if(result.Succeeded) await usrMgr.AddToRoleAsync(user, "User");

        return result.Errors;
    }

    private async Task<string> GenerateToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var roles = await usrMgr.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await usrMgr.GetClaimsAsync(user);

        var claims = new List<Claim>
        { 
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        }
        .Union(userClaims)
        .Union(roleClaims);

        var token = new JwtSecurityToken(
            issuer: cfg["JwtSettings:Issuer"],
            audience: cfg["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(cfg["JwtSettings:Duration"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<string> CreateRefreshToken()
    {
        await usrMgr.RemoveAuthenticationTokenAsync(user, LOGIN_PROVIDER, REFRESHTOKEN);
        var newRefreshToken = await usrMgr.GenerateUserTokenAsync(user, LOGIN_PROVIDER, REFRESHTOKEN);
        var result = await usrMgr.SetAuthenticationTokenAsync(user, LOGIN_PROVIDER, REFRESHTOKEN, newRefreshToken);
        return newRefreshToken;
    }

    public async Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request)
    {
        var secTokenHandler = new JwtSecurityTokenHandler();
        var tokencontent = secTokenHandler.ReadJwtToken(request.Token);
        var userName = tokencontent.Claims
            .ToList()
            .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub)?
            .Value;
        user = await usrMgr.FindByNameAsync(userName);
        // or
        // user = await usrMgr.FindByIdAsync(request.UserId);

        if (user == null || user.Id != request.UserId) return null;

        var isValidRefreshToken = await usrMgr
            .VerifyUserTokenAsync(user, LOGIN_PROVIDER, REFRESHTOKEN, request.RefreshToken);
        if(isValidRefreshToken)
        {
            var token = await GenerateToken();
            return new AuthResponseDTO
            {
                Token = token,
                UserId = user.Id,
                RefreshToken = await CreateRefreshToken()
            };
        }

        await usrMgr.UpdateSecurityStampAsync(user);
        return null;
    }
}
