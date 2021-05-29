using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Logic
{
    public class AuthV1Logic
    {
        IAuthenticationRepository repository;

        public AuthV1Logic(AuthenticationContext context)
        {
            this.repository = new AuthenticationRepository(context);
        }

        private User DoesUserWithEmailAlreadyExist(string email)
        {
            User user = repository.GetUserByEmail(email);
            if (user != null)
                return user;
            return null;
        }
    }
}
