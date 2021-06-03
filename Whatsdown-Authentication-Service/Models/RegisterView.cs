using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Models
{
    public class RegisterView
    {
        public string Email { get;  set;}
        public string Password { get;  set; }
        public string ConfirmPassword { get;  set; }
        public string DisplayName { get;  set; }
        public string Gender { get;  set; }

        public RegisterView(string email, string password, string confirmPassword, string displayName, string gender)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            DisplayName = displayName;
            Gender = gender;
        }

        public RegisterView()
        {
        }
    }
}
