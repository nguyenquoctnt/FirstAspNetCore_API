using System;

namespace FirstAspNetCore_Model
{
    public class InvalidArgumentBodyException : Exception
    {
        public InvalidArgumentBodyException()
        {
        }

        public InvalidArgumentBodyException(string message) : base(message)
        {
        }

        public InvalidArgumentBodyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
