using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger logger;
        public AuthV1Logic(IServiceProvider factory , ILogger<AuthV1Logic> _logger)
        {
            this.repository = new AuthenticationRepository(factory.CreateScope().ServiceProvider.GetRequiredService<AuthenticationContext>());
            this.logger = _logger;
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
            try
            {
                logger.LogDebug("Email: " + model.email);
                Console.WriteLine("Email: " + model.email);
                var account = repository.GetUserByEmail(model.email);
                logger.LogDebug($"Attempting to authenticate user with email: {1}", model.email);

                Console.WriteLine($"Attempting to authenticate user with email: {model.email}", model.email);
                // check account found and verify password
             
               
                if (account == null )
                {
                    // authentication failed
                    logger.LogWarning($"authentication failed with with email: {1}", model.email);
                    Console.WriteLine($"authentication failed with with email: {1}", model.email);
                    throw new ArgumentException("The email or password is incorrect.");
                }
               
                bool test = BCrypt.Net.BCrypt.Verify(model.password, account.PasswordSalt);
                if (!test)
                {
                    logger.LogWarning($"authentication failed with with email: {1}", model.email);
                    Console.WriteLine($"authentication failed with with email: {1}", model.email);
                    throw new ArgumentException("The email or password is incorrect.");
                }
                else
                {
                    Console.WriteLine("Getting Profile using userID");
                    try
                    {
                        return repository.GetProfileByUserId(account.UserID);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    return null;
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong");
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
                Console.WriteLine($"attempted to create account null values.");
                throw new ArgumentException("Please fill in all the fields");
            }

            //Check if password is long enough and if not throw exceptions
            CheckPassword(model.Password);
            //Check if email already exists and then throw exception
            if (DoesUserWithEmailAlreadyExist(model.Email))
            {
                logger.LogWarning($"attempted to create account with already existing email : {1}", model.Email);
                Console.WriteLine();
                throw new UserAlreadyExistException("This email has already been used");
            }
              



            //Hash the password 
            
            string passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(model.Password, passwordSalt);
            logger.LogInformation($"Hashing password of user: {1}", model.Email);
            // Save user

            string userID = Guid.NewGuid().ToString();
            string ProfileID = Guid.NewGuid().ToString();

            userProfile = new Profile(ProfileID, model.DisplayName, Variables.DefaultStatus,"", model.Gender, userID, null);
            user = new User(userID, model.Email, passwordHash, passwordSalt, userProfile);
            user.Profile.user = user;


            //Register new User
            repository.saveUser(user);
            Console.WriteLine($"Created account for user: ", model.Email);
            logger.LogInformation($"Created account for user: ", model.Email);
        }

    

    }
}
