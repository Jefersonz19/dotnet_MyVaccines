using Microsoft.AspNetCore.Identity;
using MyVaccine.WebaApi.Dtos;

namespace MyVaccine.WebaApi.Repositories.Contracts;

public interface IUserRepository
{
    Task<IdentityResult> AddUser(RegisterRequestDto request);
}
