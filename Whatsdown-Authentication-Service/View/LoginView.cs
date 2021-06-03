using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.View
{
    public class LoginView
    {
        public string email { get; set; }
        public string password { get; set; }

        public LoginView()
        {
        }

        public LoginView(string email, string password)
        {
            this.email = email;
            this.password = password;
        }
    }
}
