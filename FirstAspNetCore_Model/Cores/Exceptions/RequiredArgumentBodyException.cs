using System;

namespace FirstAspNetCore_Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RequiredArgumentBodyException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public RequiredArgumentBodyException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public RequiredArgumentBodyException(string message) : base(message)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        public RequiredArgumentBodyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
