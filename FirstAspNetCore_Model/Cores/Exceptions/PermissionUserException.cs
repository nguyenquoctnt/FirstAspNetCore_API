using System;

namespace FirstAspNetCore_Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PermissionUserException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public PermissionUserException()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public PermissionUserException(string message) : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public PermissionUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
