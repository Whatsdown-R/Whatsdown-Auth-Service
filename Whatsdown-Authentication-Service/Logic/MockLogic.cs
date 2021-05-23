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
        public MockLogic(AuthenticationContext _context)
        {
            this.authenticationRepository = new AuthenticationRepository(_context);
        }

        public UserViewModel MockUsers(string email)
        {
            List<User> fakeUsers = new List<User>();

            if (this.authenticationRepository.GetUserByEmail("user0@hotmail.com") == null)
            {
                for (int i = 0; i < 100; i++)
                {
                    string gui_user = Guid.NewGuid().ToString();
                    string gui_profile = Guid.NewGuid().ToString();
                    Profile profile = new Profile(gui_profile, "Testuser" + i.ToString(), "Currently inactive", null, "Female", gui_user, null);
                    User test = new User(gui_user, "user" + i.ToString() + "@hotmail.com", "nothing", "nothing", profile);
                    fakeUsers.Add(test);
                }

                authenticationRepository.saveUsers(fakeUsers);
            }
            User user;
           
            user = this.authenticationRepository.GetUserByEmail(email);
            if (user == null)
                user = this.authenticationRepository.GetUserByEmail("user0@hotmail.com");
            user.Profile = this.authenticationRepository.GetProfileByUserId(user.UserID);
         
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.email = user.Email;
            userViewModel.userId = user.UserID;
            
            userViewModel.profile = new ProfileViewModel(user.Profile.profileId, user.Profile.displayName, user.Profile.status, user.Profile.profileImage, user.Profile.gender);

            return userViewModel;



        }
    }
}
