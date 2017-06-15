using System;

namespace FirstAspNetCore_Model
{
    /// <summary>
    /// 
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public NotFoundException() : base()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public NotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public NotFoundException(ResponseStatusCode statusCode)
        {
        }
    }
}
