using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Whatsdown_Authentication_Service.Models
{
    public class Profile
    {
      
        [Key]
        public string profileId { get; set; }
        [Required]
        [MaxLength(25)]
        public string displayName { get; set; }
        [MaxLength(100)]
        public string status { get; set; }
        public string? profileImage { get; set; }
        [MaxLength(10)]
        public string? gender { get; set; }
        public string userId { get; set; }
        public User user { get; set; }


    }
}
