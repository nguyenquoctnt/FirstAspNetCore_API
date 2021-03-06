﻿using FirstAspNetCore_Help;
using Newtonsoft.Json;
using System;

namespace FirstAspNetCore_Model
{
    public class UserModel : Model
    {
        [JsonProperty(PropertyName = "id", Order = 1, NullValueHandling = NullValueHandling.Ignore)]
        public int? UserId { get; set; }

        [JsonProperty(PropertyName = "email", Order = 3, NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password", Order = 4, NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "full_name", Order = 5, NullValueHandling = NullValueHandling.Ignore)]
        public string FullName { get; set; }

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
            
            return true;
        }

    }
}
