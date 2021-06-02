using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Exceptions;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Authentication_Service.View;

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
                throw new ArgumentException("Please fill in all the fields");
            }

            //Check if password is long enough and if not throw exceptions
            CheckPassword(model.Password);
            //Check if email already exists and then throw exception
            if (DoesUserWithEmailAlreadyExist(model.Email))
                throw new UserAlreadyExistException("This email has already been used");



            //Hash the password 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
     
            // Save user

            string userID = Guid.NewGuid().ToString();
            string ProfileID = Guid.NewGuid().ToString();

            userProfile = new Profile(ProfileID, model.DisplayName, Variables.DefaultStatus, "", model.Gender, userID, null);
            user = new User(userID, model.Email, passwordHash, "Salt", userProfile);
            user.Profile.user = user;


            //Register new User
            authenticationRepository.saveUser(user);
        }

        public Profile Authenticate(LoginView model)
        {
            var account = authenticationRepository.GetUserByEmail(model.email);

            // check account found and verify password
            if (account == null || !BCrypt.Net.BCrypt.Verify(model.password, account.PasswordHash))
            {
                // authentication failed
                throw new ArgumentException("The email or password is incorrect.");
            }
            else
            {
                // authentication successful
                return authenticationRepository.GetProfileByUserId(account.UserID);
            }
        }
    



        public Profile GetUserProfile(string id)
        {
            Profile profile = authenticationRepository.GetProfileByUserId(id);
           if (profile != null)
            profile.user = null;
            return profile;
        }
       
        public List<Profile> GetProfiles(List<String> ids)
        {
            return authenticationRepository.GetProfiles(ids);
        }
        
        public void UpdateProfile(ProfileViewModel viewModel)
        {
            Profile profile = authenticationRepository.GetProfileByProfileId(viewModel.profileId);

            if (profile == null)
            {
                throw new ProfileNotFoundException("Profile does not exist");
            }

            Profile changedProfile = new Profile(profile.profileId, viewModel.displayName, viewModel.status, viewModel.profileImage, viewModel.gender, profile.UserID, profile.user);
            authenticationRepository.saveProfile(changedProfile);
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
