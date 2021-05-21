using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whatsdown_Authentication_Service.Exceptions
{
    public class ProfileNotFoundException : Exception
    {
        public ProfileNotFoundException()
        {

        }

        public ProfileNotFoundException(string message) : base(message)
        {

        }

        public ProfileNotFoundException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
