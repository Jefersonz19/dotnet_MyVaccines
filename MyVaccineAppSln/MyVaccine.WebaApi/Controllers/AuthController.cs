using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.IdentityModel.Tokens;
using MyVaccine.WebaApi.Dtos;
using MyVaccine.WebaApi.Literals;
using MyVaccine.WebaApi.Repositories.Contracts;
using MyVaccine.WebaApi.Repositories.Implementations;
using MyVaccine.WebaApi.Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyVaccine.WebaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(UserManager<IdentityUser> userManager, IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
           // var result = await _userRepository.AddUser(model);
            //if (!result.Succeeded)
            //{
              //  return BadRequest(result.Errors);
            //}
            var response = await _userService.AddUserAsync(model);
            if (response != null && response.IsSuccess) {
                return Ok(response);
            
            } else
            {
                return BadRequest(response);
            }
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)

        {
            var response = await _userService.Login(model);
            if (response != null && response.IsSuccess)
            {
                return Ok(response);

            }
            else
            {
                return Unauthorized(response);
            }

        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] LoginRequestDto model)
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            var response = await _userService.RefreshToken(claimsIdentity.Name);
            //var response = await _userService.RefreshToken(claimsIdentity.Name);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            else
            {
                return BadRequest(response);
            }
        }
    }
    
}
