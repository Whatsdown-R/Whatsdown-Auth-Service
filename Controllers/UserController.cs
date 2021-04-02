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



        // GET: api/<ValuesController>
        [HttpGet]
        public string GetUserProfiles(List<String> UserIds)
        {

            return "ds";
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public Profile GetProfile(string id)
        {
            return this.logic.GetUserProfile(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Register(RegisterModel model)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
