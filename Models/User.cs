using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Whatsdown_Authentication_Service.Models
{
    public class User
    {
    
        string UserID { get; set; }
       
        string Email { get; set; }

 
        string PasswordSalt { get; set; }
       
        string PasswordHash { get; set; }



    }
}
