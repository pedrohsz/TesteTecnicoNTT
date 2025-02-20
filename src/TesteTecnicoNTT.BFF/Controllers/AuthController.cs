using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TesteTecnicoNTT.BFF.Settings;

namespace TesteTecnicoNTT.BFF.Controllers
{
    [Route("bff/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthSettings _authSettings;

        public AuthController(IConfiguration configuration)
        {
            _authSettings = configuration.GetSection("AuthSettings").Get<AuthSettings>();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username != "admin" || request.Password != "123456")
                return Unauthorized("Credenciais inválidas");

            var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_authSettings.Secret);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                //new Claim(ClaimTypes.Role, "Admin") // role admin
            };

            var token = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
