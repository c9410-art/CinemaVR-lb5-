using CinemaVR.Api.DTO;
using CinemaVR.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinemaVR.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private const string JwtKey =
            "CinemaVR_2026_SUPER_SECRET_KEY_FOR_JWT_AUTHORIZATION_123456789";

        public AuthController(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(
                user,
                dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(
                    dto.Email);

            if (user == null)
                return Unauthorized();

            bool validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!validPassword)
                return Unauthorized();

            var claims = new List<Claim>
            {
                new Claim(
                    ClaimTypes.Name,
                    user.Email!)
            };

            var key =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(JwtKey));

            var creds =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddHours(3),
                    signingCredentials: creds);

            return Ok(new
            {
                token =
                    new JwtSecurityTokenHandler()
                        .WriteToken(token)
            });
        }
    }
}