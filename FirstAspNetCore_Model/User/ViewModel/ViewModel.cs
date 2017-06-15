using Newtonsoft.Json;

namespace FirstAspNetCore_Model
{
    public class ViewModel
    {
        [JsonProperty(PropertyName = ".username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty(PropertyName = ".hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = ".type", NullValueHandling = NullValueHandling.Ignore)]
        public UserType Type { get; set; }
        
    }
}
