﻿using Microsoft.Extensions.Logging;
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
    public class AuthV1Logic
    {
        AuthenticationRepository repository;
        private ILogger logger;
        public AuthV1Logic(AuthenticationContext context, ILogger<AuthV1Logic> logger)
        {
            this.repository = new AuthenticationRepository(context);
            this.logger = logger;
        }

        private bool DoesUserWithEmailAlreadyExist(string email)
        {
            User user = repository.GetUserByEmail(email);
            if (user != null)
                return true;
            return false;
        }

        public Profile Authenticate(LoginView model)
        {
           
            var account = repository.GetUserByEmail(model.email);
            logger.LogInformation($"Attempting to authenticate user with email: {1}", model.email);
            // check account found and verify password
            if (account == null || !BCrypt.Net.BCrypt.Verify(model.password, account.PasswordHash))
            {
                // authentication failed
                logger.LogInformation($"authentication failed with with email: {1}", model.email);
                throw new ArgumentException("The email or password is incorrect.");
            }
            else
            {
                // authentication successful
                return repository.GetProfileByUserId(account.UserID);
            }
        }

        private void CheckPassword(string password)
        {
            //Check if password is long enough and if not throw exceptions
            if (!(password.Length >= Variables.DefaultMinimumPasswordLength))
                throw new InsufficientPasswordException("Password too short. Minimum characters needed is " + Variables.DefaultMinimumPasswordLength);
            if (!(password.Length <= Variables.DefaultMaximumPasswordLength))
                throw new InsufficientPasswordException("Password too long. Maximum characters allowed is " + Variables.DefaultMaximumPasswordLength);
        }


        public void Register(RegisterModel model)
        {
            User user = null;
            Profile userProfile = null;
            if (model.DisplayName == null || model.Email == null || model.ConfirmPassword == null || model.Password == null || model.ConfirmPassword != model.Password)
            {
                //Throw Exception
                logger.LogError($"attempted to create account null values.");
                throw new ArgumentException("Please fill in all the fields");
            }

            //Check if password is long enough and if not throw exceptions
            CheckPassword(model.Password);
            //Check if email already exists and then throw exception
            if (DoesUserWithEmailAlreadyExist(model.Email))
            {
                logger.LogWarning($"attempted to create account with already existing email : {1}", model.Email);
                throw new UserAlreadyExistException("This email has already been used");
            }
              



            //Hash the password 
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);
            logger.LogInformation($"Hashing password of user: {1}", model.Email);
            // Save user

            string userID = Guid.NewGuid().ToString();
            string ProfileID = Guid.NewGuid().ToString();

            userProfile = new Profile(ProfileID, model.DisplayName, Variables.DefaultStatus, "", model.Gender, userID, null);
            user = new User(userID, model.Email, passwordHash, "Salt", userProfile);
            user.Profile.user = user;


            //Register new User
            repository.saveUser(user);
            logger.LogInformation($"Created account for user: ", model.Email);
        }

    

    }
}