using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Models
{
    public class ProfileViewModel
    {

        public string profileId { get; private set; }
        public string displayName { get; private set; }
        public string status { get; private set; }
        public string? profileImage { get; private set; }
        public string? gender { get; private set; }

        public ProfileViewModel()
        {
        }

        public ProfileViewModel(string profileId, string displayName, string status, string profileImage, string gender)
        {
            this.profileId = profileId;
            this.displayName = displayName;
            this.status = status;
            this.profileImage = profileImage;
            this.gender = gender;
        }
    }
}
