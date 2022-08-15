using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Suggestion.Shared.Model.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Suggestion.Server.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("api/login")]
        public IActionResult Login([FromBody] LoginParameters login)
        {
            if (login.Email == "a" && login.Password == "a")
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, login.Email),
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim(ClaimTypes.Role, "RoleHelloo"),
                    new Claim(ClaimTypes.NameIdentifier, "NameIdentifier"),
                    new Claim(ClaimTypes.GivenName, "GivenName")
                };

                var user = Authenticate(login);

                if (user != null)
                {

                    var token = Generate(user, claims);
                    return Ok(new JwToken { token = token });

                }
                //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
                //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

                //var token = new JwtSecurityToken(
                //    _configuration["JwtIssuer"],
                //    _configuration["JwtIssuer"],
                //    claims,
                //    expires: expiry,
                //    signingCredentials: creds
                //);

            }

            return BadRequest("Username and password are invalid.");
        }

        private string Generate(LoginResult user, Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            //var claims = new[] {

            //    new Claim(ClaimTypes.NameIdentifier,user.UserName)

            //};
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                "https://localhost:7168/",
                claims,
                expires: DateTime.Now.AddSeconds(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private LoginResult Authenticate(LoginParameters loginDetails)
        {
            return new LoginResult { Email = "aghamoeez@gmail.com", Role = "Admin", UserName = "Agha" };
        }
    }
}
