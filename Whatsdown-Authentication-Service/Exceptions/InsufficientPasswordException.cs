using System;


namespace Whatsdown_Authentication_Service.Exceptions
{
    public class InsufficientPasswordException : Exception
    {
        public InsufficientPasswordException()
        {

        }

        public InsufficientPasswordException(string message) : base(message)
        {

        }

        public InsufficientPasswordException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}
