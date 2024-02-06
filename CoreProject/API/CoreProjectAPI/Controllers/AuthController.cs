using CoreProjectAPI.Models.DTO.Auth;
using CoreProjectAPI.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(
        UserManager<IdentityUser> userManager,
        ITokenRepository tokenRepository) : ControllerBase
    {
        // POST: 'http://localhost:5204/api/auth/register'
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            var user = new IdentityUser
            {
                UserName = request.Email.Trim(),
                Email = request.Email.Trim()
            };
            var result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                result = await userManager.AddToRoleAsync(user, "Reader");
                if (result.Succeeded)
                {
                    return Ok();
                }

                if (!result.Errors.Any()) return ValidationProblem(ModelState);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            else
            {
                if (!result.Errors.Any()) return ValidationProblem(ModelState);
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return ValidationProblem(ModelState);
        }

        // POST: 'http://localhost:5204/api/auth/login'
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, request.Password);
                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var jwtToken = tokenRepository.CreateJwtToken(user, roles.ToList());
                    var response = new LoginResponseDto(
                        Email: request.Email,
                        Roles: roles.ToList(),
                        Token: jwtToken);
                    return Ok(response);
                }
            }

            ModelState.AddModelError("", "Email or Password is Incorrect");
            return ValidationProblem(ModelState);
        }
    }
}