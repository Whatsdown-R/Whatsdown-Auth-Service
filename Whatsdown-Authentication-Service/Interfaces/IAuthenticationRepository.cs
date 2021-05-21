using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service
{
    interface IAuthenticationRepository
    {
        public void saveUser(User User);
        public void saveProfile(Profile User);

        public User GetUserByEmail(string Email);

        public List<Profile> GetProfiles(List<String> ids);
    }
}
