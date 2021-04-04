using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Data
{
    public class AuthenticationRepository : IAuthenticationRepository
    {

        AuthenticationContext authenticationContext; 

        public AuthenticationRepository(AuthenticationContext auth)
        {

            this.authenticationContext = auth;
        }

        public User GetUserByEmail(string Email)
        {
            return authenticationContext.Users.SingleOrDefault<User>(x => x.Email == Email);
        }

        public Profile GetProfileByUserId(string Id)
        {
            return authenticationContext.Profiles.SingleOrDefault<Profile>(p => p.UserID == Id);
        }

        public Profile GetProfileByProfileId(string ProfileID)
        {
            return authenticationContext.Profiles.SingleOrDefault<Profile>(p => p.profileId == ProfileID );
        }
        public List<Profile> GetProfiles(List<string> ids)
        {
            return this.authenticationContext.Profiles.Where(profile => ids.Contains(profile.UserID)).ToList();
        }

        public void saveProfile(Profile userProfile)
        {
            throw new NotImplementedException();
        }

        public void saveUser(User user)
        {
            authenticationContext.Users.Add(user);
        }

    
    }
}
