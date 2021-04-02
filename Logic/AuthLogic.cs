using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Logic
{
    public class AuthLogic
    {
        AuthenticationRepository authenticationRepository;
        public AuthLogic(AuthenticationContext _context)
        {
            this.authenticationRepository = new AuthenticationRepository(_context);
        }

        public void Register(RegisterModel model)
        {
            User user = null;
            Profile userProfile = null;
            if(model.DisplayName == null || model.Email == null || model.ConfirmPassword == null || model.Password == null)
            {
                //Throw Exception
                return;
            }


            //Check if email already exists and then throw exception
            if (DoesUserWithEmailAlreadyExist(model.Email))
                return;

            string userID = Guid.NewGuid().ToString();
            string ProfileID = Guid.NewGuid().ToString();
            user = new User(userID, model.Email, model.ConfirmPassword, model.ConfirmPassword, userProfile);


            //Register new User
            authenticationRepository.saveUser(user);
        }

        private bool DoesUserWithEmailAlreadyExist(string email)
        {
            User user = authenticationRepository.GetUserByEmail(email);
            if (user != null)
                return true;
            return false;
        }
    }
}
