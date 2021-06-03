using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Models
{
    public class JWT
    {
        public string id { get; set; }
        public string role { get; set; }
        public string token { get; set; }

        public JWT(string id, string role, string token)
        {
            this.id = id;
            this.role = role;
            this.token = token;
        }
    }
}
