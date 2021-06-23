using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Models
{
    public class CreateProfile
    {
        public string ProfileId { get; set; }
        public string DisplayName { get; set; }

        public string Gender { get; set; }


        public CreateProfile()
        {

        }

        public CreateProfile(string profileId, string displayName, string gender)
        {
            ProfileId = profileId;
            DisplayName = displayName;
            Gender = gender;
        }
    }
}
