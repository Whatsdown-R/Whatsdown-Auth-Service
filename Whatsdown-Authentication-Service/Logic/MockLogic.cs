using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Data;
using Whatsdown_Authentication_Service.Models;
using Whatsdown_Authentication_Service.View;

namespace Whatsdown_Authentication_Service.Logic
{
    public class MockLogic
    {
        AuthenticationRepository authenticationRepository;
        private readonly ILogger<MockLogic> logger;
        public MockLogic(AuthenticationContext _context, ILogger<MockLogic> logger)
        {
            this.authenticationRepository = new AuthenticationRepository(_context);
            this.logger = logger;
        }

        public UserViewModel MockUsers(string email)
        {
            try
            {
                List<User> fakeUsers = new List<User>();
                this.logger.LogInformation($"Email is {1}", email);
                this.logger.LogInformation("Checking if users already exist");
                if (this.authenticationRepository.GetUserByEmail("user0@hotmail.com") == null)
                {
                    this.logger.LogInformation("users do not exist");
                    for (int i = 0; i < 100; i++)
                    {
                        string gui_user = Guid.NewGuid().ToString();
                        string gui_profile = Guid.NewGuid().ToString();
                        Profile profile = new Profile(gui_profile, "Testuser" + i.ToString(), "Currently inactive", null, "Female", gui_user, null);
                        User test = new User(gui_user, "user" + i.ToString() + "@hotmail.com", "nothing", "nothing", profile);
                        fakeUsers.Add(test);
                    }

                    authenticationRepository.saveUsers(fakeUsers);
                    this.logger.LogInformation("Created new users");
                }
                User user;

                user = this.authenticationRepository.GetUserByEmail(email);
                this.logger.LogInformation($"Check if user with email {1} exists", email);
                if (user == null)
                {
                    this.logger.LogInformation("User does not exist with email so give him the email: user0@hotmail.com");
                    user = this.authenticationRepository.GetUserByEmail("user0@hotmail.com");
                }
                this.logger.LogInformation("Attempting to get profile");
                this.logger.LogInformation(user.ToString());

                user.Profile = this.authenticationRepository.GetProfileByUserId(user.UserID);
                this.logger.LogInformation("Got profile");

                UserViewModel userViewModel = new UserViewModel();
                userViewModel.email = user.Email;
                userViewModel.userId = user.UserID;

                userViewModel.profile = new ProfileViewModel(user.Profile.profileId, user.Profile.displayName, user.Profile.status, user.Profile.profileImage, user.Profile.gender);
                this.logger.LogInformation("Return profile");
                return userViewModel;
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.logger.LogError(ex.Message);
            }
            return null;
        }
    }
}
