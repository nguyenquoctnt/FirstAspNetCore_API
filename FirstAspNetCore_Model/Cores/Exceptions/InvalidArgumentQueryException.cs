using System;

namespace FirstAspNetCore_Model
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidArgumentQueryException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public InvalidArgumentQueryException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public InvalidArgumentQueryException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public InvalidArgumentQueryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
