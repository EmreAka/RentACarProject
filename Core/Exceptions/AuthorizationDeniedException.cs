using System;

namespace Core.Exceptions
{
    public class AuthorizationDeniedException: Exception
    {
        public AuthorizationDeniedException()
        {
        }

        public AuthorizationDeniedException(string message)
            : base(message)
        {
        }

        public AuthorizationDeniedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}