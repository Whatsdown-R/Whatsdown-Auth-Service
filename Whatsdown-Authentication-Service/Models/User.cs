using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Whatsdown_Authentication_Service.Models
{

    [Table("UserInfo")]
    public class User
    {

        [Key]
        public string UserID { get;  set; }

        [Required]
        public string Email { get;  set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string PasswordSalt { get;  set; }
        [Required]
        public string PasswordHash { get;  set; }

        [MaxLength(75)]
        [Required]
        public string ProfileId { get; set; }

        public User() { }

        public User(string userID, string email, string passwordSalt, string passwordHash, string profile, string role)
        {
            UserID = userID;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            Role = role;
            ProfileId = profile;
        }
    }
}
