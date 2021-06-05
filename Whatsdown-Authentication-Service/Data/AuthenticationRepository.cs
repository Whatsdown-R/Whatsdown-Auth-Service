
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Data
{
    public class AuthenticationRepository  
    {
        private readonly ILogger _logger;
        AuthenticationContext authenticationContext; 

        public AuthenticationRepository(AuthenticationContext auth)
        {
        
            this.authenticationContext = auth;
        }

        public User GetUserByEmail(string Email)
        {
            return authenticationContext.UserInfo.FirstOrDefault<User>(x => x.Email == Email);
        }

 



        public void saveUser(User user)
        {
                authenticationContext.UserInfo.Add(user);
                authenticationContext.SaveChanges();

  
        }
        public void saveUsers(List<User> users)
        {
            authenticationContext.UserInfo.AddRange(users);
            authenticationContext.SaveChanges();
        }


    }
}
