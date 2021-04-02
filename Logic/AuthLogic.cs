using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Exceptions;
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
            if(model.DisplayName == null || model.Email == null || model.ConfirmPassword == null || model.Password == null || model.ConfirmPassword != model.Password)
            {
                //Throw Exception
                return;
            }

            //Check if password is long enough and if not throw exceptions
            CheckPassword(model.Password);
            //Check if email already exists and then throw exception
            if (DoesUserWithEmailAlreadyExist(model.Email))
                throw new UserAlreadyExistException("This email has already been used");

       

            //Hash the password and get Salt


            // Save user

            string userID = Guid.NewGuid().ToString();
            string ProfileID = Guid.NewGuid().ToString();

            userProfile = new Profile(ProfileID, model.DisplayName, Variables.DefaultStatus, "", model.Gender, userID, null);
            user = new User(userID, model.Email, "Hash", "Salt", userProfile);
            user.Profile.user = user;


            //Register new User
            authenticationRepository.saveUser(user);
        }

        public Profile GetUserProfile(string id)
        {
            Profile profile = authenticationRepository.GetProfileByUserId(id);
            profile.user = null;
            return profile;
        }
       
        public List<Profile> GetProfiles(List<String> ids)
        {
            return authenticationRepository.GetProfiles(ids);
        }
        
        
        
        
        
        private bool DoesUserWithEmailAlreadyExist(string email)
        {
            User user = authenticationRepository.GetUserByEmail(email);
            if (user != null)
                return true;
            return false;
        }

        private void CheckPassword(string password)
        {
            //Check if password is long enough and if not throw exceptions
            if (!(password.Length >= Variables.DefaultMinimumPasswordLength))
                throw new InsufficientPasswordException("Password too short. Minimum characters needed is " + Variables.DefaultMinimumPasswordLength);
            if (!(password.Length <= Variables.DefaultMaximumPasswordLength))
                throw new InsufficientPasswordException("Password too long. Maximum characters allowed is " + Variables.DefaultMaximumPasswordLength);
        }
    }
}
