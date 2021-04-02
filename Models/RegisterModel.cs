using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Models
{
    public class RegisterModel
    {
        public string Email { get; private set;}
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
        public string DisplayName { get; private set; }
        public string Gender { get; private set; }
    }
}
