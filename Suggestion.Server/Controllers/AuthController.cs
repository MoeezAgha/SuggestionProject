using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Suggestion.Shared.Model.ViewModel;
using System.Security.Claims;

namespace Suggestion.Server.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]

    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Index()
        {
            return Ok("HI");
        }
        [HttpGet("private")]
        [Authorize]

        public IActionResult Indprivateex()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var userClaims = identity.Claims;

                var z = new LoginResult
                {
                    Email = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value

                };

            }
            return Ok("private");
        }
    }
}
