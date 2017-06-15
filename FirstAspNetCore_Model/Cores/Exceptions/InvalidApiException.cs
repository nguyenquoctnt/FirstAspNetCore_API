using System;

namespace FirstAspNetCore_Model
{
    /// <summary>
    /// 
    /// </summary>
    public class InvalidApiException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public InvalidApiException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public InvalidApiException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public InvalidApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
