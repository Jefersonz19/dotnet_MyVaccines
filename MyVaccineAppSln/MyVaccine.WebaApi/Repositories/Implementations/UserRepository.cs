using Microsoft.AspNetCore.Identity;
using MyVaccine.WebaApi.Dtos;
using MyVaccine.WebaApi.Models;
using MyVaccine.WebaApi.Repositories.Contracts;
using System.Security.Cryptography;
using System.Transactions;

namespace MyVaccine.WebaApi.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly MyVaccineAppDbContext _context;

    private readonly UserManager<IdentityUser> _userManager;
    public UserRepository(MyVaccineAppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;   
        _userManager = userManager;
    }
    public async Task<IdentityResult> AddUser(RegisterRequestDto request)
    {
        var response = new IdentityResult();

        using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled)) 
        {
            var user = new ApplicationUser
            {
                UserName = request.Username.ToLower(),
                Email = request.Username
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            response = result;

            if (!result.Succeeded) 
            {
                return response;
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                AspNetUserId = user.Id,
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
            scope.Complete();
        }

        return response;
    }

}
