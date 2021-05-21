﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Logic;

namespace Whatsdown_Authentication_Service.Controllers
{
    [Route("api/mock")]
    [ApiController]
    public class MockController : ControllerBase
    {
        MockLogic logic;
        public MockController(AuthenticationContext auth)
        {
            this.logic = new MockLogic(auth);
        }

        [HttpGet(), Route("{id}")]
        public IActionResult MockData(string id)
        {
            IActionResult response = Unauthorized();

            var profile = this.logic.MockUsers(id);
            response = Ok(new { user = profile });

            return response;
        }

    }
}