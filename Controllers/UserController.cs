using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Logic;
using Whatsdown_Authentication_Service.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Whatsdown_Authentication_Service.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        AuthLogic logic;
        public UserController(AuthenticationContext auth)
        {
            this.logic = new AuthLogic(auth);
        }



       
        [HttpGet]
        public IActionResult GetUserProfiles(List<String> UserIds)
        {
            IActionResult response = Unauthorized();
            var profiles = logic.GetProfiles(UserIds);
            response = Ok(new { profiles = profiles });
            return response;
        }

      
        [HttpGet("{id}")]
        public IActionResult GetProfile(string id)
        {
            IActionResult response = Unauthorized();
            
            var profile = this.logic.GetUserProfile(id);
            response = Ok(new { profiles = profile });
          
            return response;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            IActionResult response = Unauthorized();
            logic.Register(model);
            response = Ok();
            return response;
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public IActionResult UpdateProfile(ProfileViewModel viewModel)
        {
            IActionResult response = Unauthorized();
            logic.UpdateProfile(viewModel);
            response = Ok();
            return response;
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
