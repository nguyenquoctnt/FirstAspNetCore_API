namespace FirstAspNetCore_Model
{
    public interface IRequestHeaderModel
    {/// <summary>
     /// Gets or sets request time.
     /// </summary>
        long Time { get; set; }
        /// <summary>
        /// Gets or sets request agent of device.
        /// </summary>
        string UserAgent { get; set; }
        /// <summary>
        /// Gets or sets request language.
        /// </summary>
        string Language { get; set; }
    }
}
