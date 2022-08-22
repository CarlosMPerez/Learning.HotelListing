using HotelListing.API.DTOs;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.API.Contracts;

public interface IAuthManager
{
    Task<IEnumerable<IdentityError>> Register(APIUserDTO userDTO);

    Task<AuthResponseDTO> Login(LoginUserDTO login);

    Task<string> CreateRefreshToken();

    Task<AuthResponseDTO> VerifyRefreshToken(AuthResponseDTO request);
}
