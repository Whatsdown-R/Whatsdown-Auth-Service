using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Whatsdown_Authentication_Service.Models
{
    public class User
    {

        public string UserID { get; private set; }

        public string Email { get; private set; }


        public string PasswordSalt { get; private set; }

        public string PasswordHash { get; private set; }

        public Profile Profile { get; private set; }

        public User() { }

        public User(string userID, string email, string passwordSalt, string passwordHash, Profile profile)
        {
            UserID = userID;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Profile = profile;
        }
    }
}
