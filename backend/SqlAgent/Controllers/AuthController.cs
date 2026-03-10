using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SqlAgent.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            // Demo users
            if (request.Username == "user" && request.Password == "123")
                return Ok(GenerateToken("USER"));

            if (request.Username == "admin" && request.Password == "123")
                return Ok(GenerateToken("ADMIN"));

            if (request.Username == "superadmin" && request.Password == "123")
                return Ok(GenerateToken("SUPERADMIN"));

            return Unauthorized();
        }

        private string GenerateToken(string role)
        {
            var jwt = _config.GetSection("Jwt");

            var claims = new[]
            {
            new Claim(ClaimTypes.Role, role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public record LoginRequest(string Username, string Password);
}
