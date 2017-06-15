namespace FirstAspNetCore_Model
{
    /// <summary>
    /// Provides enumerated values to use to set response status.
    /// </summary>
    public enum ResponseStatusCode : int
    {
        /// <summary>
        /// Specifies case success.
        /// </summary>
        OK = 200,
        /// <summary>
        /// Specifies case failure.
        /// </summary>
        FAILURE = 400,
        /// <summary>
        /// Specifies case not found.
        /// </summary>
        NOT_FOUND = 404,
        /// <summary>
        /// Specifies case api session.
        /// </summary>
        API_SESSION = 102,
        /// <summary>
        /// Specifies case unknown api.
        /// </summary>
        API_UNKNOWN = 1,
        /// <summary>
        /// Specifies case api service.
        /// </summary>
        API_SERVICE = 2,
        /// <summary>
        /// Specifies case too many call to api.
        /// </summary>
        API_TOO_MANY_CALLS = 4,
        /// <summary>
        /// Specifies case user too many call to api.
        /// </summary>
        API_USER_TOO_MANY_CALLS = 17,
        /// <summary>
        /// Specifies case permission of api has been denied.
        /// </summary>
        API_PERMISSION_DENIED = 10,
        /// <summary>
        /// Specifies case api limit reached.
        /// </summary>
        API_LIMIT_REACHED = 341,
        /// <summary>
        /// Specifies case api access has been denied.
        /// </summary>
        API_ACCESS_DENIDED = 357,
        /// <summary>
        /// Specifies case app not installed.
        /// </summary>
        APP_NOT_INSTALLED = 458,
        /// <summary>
        /// Specifies case api has expired.
        /// </summary>
        API_EXPIRED = 463,
        /// <summary>
        /// Specifies case password of user has been changed.
        /// </summary>
        PASSWORD_CHANGED = 460,
        /// <summary>
        /// Specifies case user has been checkpointed.
        /// </summary>
        USER_CHECKPOINTED = 459,
        /// <summary>
        /// Specifies case user unconfirmed.
        /// </summary>
        USER_UNCONFIRMED = 464,
        /// <summary>
        /// Specifies case invalid access token.
        /// </summary>
        INVALID_ACCESS_TOKEN = 467,
        /// <summary>
        /// Specifies case invalid arguments.
        /// </summary>
        INVALID_ARGUMENTS = 502,
        /// <summary>
        /// Specifies case required arguments are missing.
        /// </summary>
        REQUIRED_ARGUMENTS = 529,
        /// <summary>
        /// Specifies case internal server error.
        /// </summary>
        INTERNAL_SERVER = 500,
        /// <summary>
        /// Specifies case network error.
        /// </summary>
        NETWORK_ERROR = 600
    }
}
