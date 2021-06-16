using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Exceptions;
using Whatsdown_Authentication_Service.Logic;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Authentication_Service.View;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace Whatsdown_Authentication_Service.Controllers
{

    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private AuthV1Logic logic;
        private readonly ILogger logger;
        private readonly ILogger testLogger;
        public AuthController(IConfiguration configuration,  ILogger<AuthController> logger, AuthV1Logic logic)
        {
            this._configuration = configuration;
            this.logic = logic;
            this.logger = logger;
            testLogger = ApplicationLogging.CreateLogger<AuthController>();
        }

        [HttpPost(), Route("google")]    
        public async Task<IActionResult> GoogleLogin(string idToken)
        {
            try
            {
               
                logger.LogDebug("Attempting to login with google account");
                logger.LogInformation(_configuration.GetSection("Authentication:Google:ClientId").Value);
                Payload payload = await ValidateAsync(idToken, new ValidationSettings
                {
                    Audience = new[] { _configuration.GetSection("Authentication:Google:ClientId").Value }
                });
                logger.LogInformation("Hosted Domain: " + payload.HostedDomain);
                if (payload.EmailVerified)
                {
                    
                    if (!logic.DoesUserWithEmailAlreadyExist(payload.Email))
                    {
                        await logic.RegisterGoogleAccountAsync(new RegisterView(payload.Email, "", "", payload.Name + " " + payload.FamilyName, "Unknown"));
                    }
                    JWT jwt = logic.GoogleAuthenticate(payload.Email);

                    return Ok(jwt);
                }
                logger.LogInformation($"Google email was not verified, {1}", payload.Email);
                //store user

                return Unauthorized("Please verify google account first.");
                
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex.Message);
                return Unauthorized();
            }
        }

        [HttpPost(), Route("normal")]
        public IActionResult Authenticate(LoginView model)
        {
            IActionResult response = Unauthorized();
   
            try
            {
                return Ok(new { jwt = logic.Authenticate(model) });
            }
            catch (ArgumentException ex)
            {
               return BadRequest(ex.Message);
            }catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }

       
        }


       

        [HttpPost(), Route("register")]
        public IActionResult Register([FromBody]RegisterView model)
        {
            IActionResult response;
            try
            {
                logger.LogDebug("The register method has been called");
                string profileId = logic.RegisterAsync(model).Result;

                response = Ok(new { response = profileId });
            }
            catch(ArgumentException ex)
            {
                response = BadRequest(ex.Message);
            }catch(UserAlreadyExistException ex)
            {
                response = BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                response = Unauthorized(ex.Message);
            }
                return response;
        }
    }


}
