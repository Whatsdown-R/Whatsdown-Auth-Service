using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.View
{
    public class UserViewModel
    {
        public string email { get; set; }
        public string userId { get; set; }
        public ProfileViewModel profile { get; set; }

        public UserViewModel()
        {
        }
    }
}
