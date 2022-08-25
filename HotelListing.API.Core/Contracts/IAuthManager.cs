using Microsoft.AspNetCore.Identity;
using HotelListing.API.Core.DTOs;

namespace HotelListing.API.Core.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO);

    Task<AuthResponseDTO> Login(LoginUserDTO login);

    Task<string> CreateRefreshToken();

    Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request);

    Task<IEnumerable<IdentityError>> PromoteToAdmin(string userName);

    Task<IEnumerable<IdentityError>> DemoteToUser(string userName);
}
