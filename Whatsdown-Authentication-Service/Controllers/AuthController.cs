﻿using Microsoft.AspNetCore.Http;
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
        private ILogger<AuthController> logger;
        public AuthController(IConfiguration configuration,  ILogger<AuthController> logger, AuthV1Logic logic)
        {
            this._configuration = configuration;
            this.logic = logic;
            this.logger = logger;
        }

           [HttpPost("google")]    
        public async Task<IActionResult> GoogleLogin(string idToken)
        {
            try
            {
                logger.LogDebug("Attempting to login with google account");
                Payload payload = await ValidateAsync(idToken, new ValidationSettings
                {
                    Audience = new[] { _configuration.GetSection("Authentication:Google")["ClientId"] }
                });

                if (payload.EmailVerified)
                {
                    
                    var user = new { name = "Test", lastname = "Tester" };

                    return Ok(user);
                }
                logger.LogInformation($"Google email was not verified, {1}", payload.Email);
                //store user

                return Unauthorized("Please verify google account first.");
                
            }
            catch
            {
                // Invalid token
                return Unauthorized();
            }
        }

        [HttpPost("normal")]
        public IActionResult Authenticate(LoginView model)
        {
            IActionResult response = Unauthorized();
            logger.LogDebug("Calling the authenticate method.");
            Console.WriteLine($"Calling the authenticate method.");
            try
            {
                return Ok(new { token = logic.Authenticate(model) });
            }
            catch (ArgumentException ex)
            {
                Unauthorized(ex.Message);
            }

            return response;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model)
        {
            IActionResult response = Unauthorized();
            try
            {
                logic.Register(model);
                response = Ok("Succesfully created account");
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
