using FirstAspNetCore_Help;
using Newtonsoft.Json;
using System;

namespace FirstAspNetCore_Model
{
    public class UserModel : Model
    {
        [JsonProperty(PropertyName = "id", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "user_type", Order = 2, NullValueHandling = NullValueHandling.Ignore)]
        public UserType? UserType { get; set; }

        [JsonProperty(PropertyName = "email", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "full_name", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public string FullName { get; set; }
        [JsonProperty(PropertyName = "phone", Order = 9, NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty(PropertyName = "address", Order = 10, NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }

        protected override bool CheckValidate(bool isNew = false)
        {
            if (string.IsNullOrEmpty(Email))
                throw new RequiredArgumentBodyException("email");
            if (!ValidateHelper.ValidateEmail(Email))
                throw new InvalidArgumentBodyException("email");
            if (string.IsNullOrEmpty(Password))
                throw new RequiredArgumentBodyException("password");
            if (string.IsNullOrEmpty(FullName))
                throw new RequiredArgumentBodyException("full_name");

            if (!isNew)
                if (!UserId.HasValue)
                    throw new RequiredArgumentBodyException("id");

            return true;
        }

        public void GenerateHashedPassword()
        {
            //if (!string.IsNullOrEmpty(Password))
            //    Password = Crypto.Encrypt(Password, PassPhrase, SaltValue, InitVector);
        }

        public void GenerateHashed()
        {
            //GeneratePassPharse();
            //GenerateSaltValue();
            //GenerateInitVector();
            //GenerateClientToken();
            GenerateHashedPassword();
        }

    }
}
