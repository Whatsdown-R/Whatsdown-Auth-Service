using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Whatsdown_Authentication_Service.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

           [HttpPost("google")]    
        public async Task<IActionResult> GoogleLogin(string idToken)
        {
            try
            {
                Payload payload = await ValidateAsync(idToken, new ValidationSettings
                {
                    Audience = new[] { _configuration.GetSection("Authentication:Google")["ClientId"] }
                });

                if (payload.EmailVerified)
                {
                    
                    var user = new { name = "Test", lastname = "Tester" };

                    return Ok(user);
                }
                //store user

                return Unauthorized("Please verify google account first.");
                
            }
            catch
            {
                // Invalid token
                return Unauthorized();
            }
        }
    }
}
