using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whatsdown_Authentication_Service.Models;

namespace Whatsdown_Authentication_Service.Data
{
    public class AuthenticationContext : DbContext
    {
        public DbSet<User> UserInfo { get; set; }
       
        public AuthenticationContext(DbContextOptions<AuthenticationContext> options) : base(options)
        {
            if(!Database.IsInMemory())
                Database.EnsureCreated();
        
        }

  




    }
}
