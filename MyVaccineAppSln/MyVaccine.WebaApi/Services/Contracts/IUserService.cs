using MyVaccine.WebaApi.Dtos;

namespace MyVaccine.WebaApi.Services.Contracts;

public interface IUserService
{
    Task<AuthResponseDto> AddUserAsync(RegisterRequestDto request);

    Task<AuthResponseDto> Login(LoginRequestDto request);

    Task<AuthResponseDto> RefreshToken(string email);

}
