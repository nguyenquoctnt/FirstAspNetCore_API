using Newtonsoft.Json;

namespace FirstAspNetCore_Model
{
    /// <summary>
    /// Represents the response model after executed request.
    /// </summary>
    public class ResponseModel<T>
    {
        /// <summary>
        /// Gets or sets status code of response.
        /// </summary>
        [JsonProperty(PropertyName = "code", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public ResponseStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets status of response.
        /// </summary>
        [JsonProperty(PropertyName = "status", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets message of response.
        /// </summary>
        [JsonProperty(PropertyName = "message", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets response data.
        /// </summary>
        [JsonProperty(PropertyName = "data", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets response total data.
        /// </summary>
        [JsonProperty(PropertyName = "total", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public int? Total { get; set; }

        public ResponseModel() { }

        public ResponseModel(string message)
        {
            if (message == null)
                message = string.Empty;

            Message = message;
            Status = !string.IsNullOrEmpty(message) ? false : true;
            StatusCode = Status ? ResponseStatusCode.OK : ResponseStatusCode.FAILURE;
        }

        public ResponseModel(T data)
        {
            Data = data;
            Status = true;
            StatusCode = ResponseStatusCode.OK;
        }

        public ResponseModel(T data, bool status)
        {
            Data = data;
            Status = status;
            StatusCode = status ? ResponseStatusCode.OK : ResponseStatusCode.FAILURE;
        }

        public ResponseModel(T data, bool status, ResponseStatusCode statusCode)
        {
            Data = data;
            Status = status;
            StatusCode = statusCode;
        }

        public ResponseModel(string message, T data)
        {
            if (message == null)
                message = string.Empty;

            Message = message;
            Status = !string.IsNullOrEmpty(message) ? false : true;
            StatusCode = Status ? ResponseStatusCode.OK : ResponseStatusCode.FAILURE;
            Data = data;
        }

        public ResponseModel(string message, bool status, ResponseStatusCode statusCode)
        {
            if (message == null)
                message = string.Empty;

            Message = message;
            status = !string.IsNullOrEmpty(message) ? false : true;
            StatusCode = statusCode;
        }

        public ResponseModel(string message, T data, ResponseStatusCode statusCode)
        {
            if (message == null)
                message = string.Empty;

            Message = message;
            Status = !string.IsNullOrEmpty(message) ? false : true;
            StatusCode = statusCode;
            Data = data;
        }
    }
}
