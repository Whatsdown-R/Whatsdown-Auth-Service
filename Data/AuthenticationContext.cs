using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Data
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Profile> profiles { get; set; }

       
    }
}
