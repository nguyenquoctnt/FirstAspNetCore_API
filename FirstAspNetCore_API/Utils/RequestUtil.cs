using FirstAspNetCore_Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace FirstAspNetCore_API
{
    public class RequestUtil
    {
        public static RequestHeaderModel GetRequestHeader(HttpRequest request)
        {
            RequestHeaderModel result = new RequestHeaderModel
            {
                Language = GetHeaderValueByKey("language", request.Headers, "vi"),
                UserLoginId = GetHeaderValueByKey("user_id", request.Headers, -1),
                UserPasswordLogin = GetHeaderValueByKey("user_password", request.Headers, string.Empty),
            };
            
            return result;
        }

        
        public static T GetHeaderValueByKey<T>(string key, IHeaderDictionary headers, T defaultValue)
        {
            var values = headers[key];
            if (values.Count > 0)
                return (T)Convert.ChangeType(values.FirstOrDefault(), typeof(T));

            return defaultValue;
        }
    }
}
