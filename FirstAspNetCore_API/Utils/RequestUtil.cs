using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using FirstAspNetCore_Help;
using FirstAspNetCore_Model;

namespace FirstAspNetCore_API
{
    public class RequestUtil
    {
        public static RequestHeaderModel GetRequestHeader(HttpRequest request)
        {
            RequestHeaderModel result = new RequestHeaderModel
            {
                Time = GetHeaderValueByKey("time_stamp", request.Headers, -1L),
                UserAgent = GetHeaderValueByKey("User-Agent", request.Headers, string.Empty),
                Language = GetHeaderValueByKey("language", request.Headers, "vi"),
                UserLoginId = GetHeaderValueByKey("user_id", request.Headers, -1),
                UserTypeLoginId = GetHeaderValueByKey("user_type_id", request.Headers, -1),
                UserPasswordLogin = GetHeaderValueByKey("user_password", request.Headers, string.Empty),
            };
            
            return result;
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="headers"></param>
        /// <returns></returns>
        public static T GetHeaderValueByKey<T>(string key, IEnumerable<KeyValuePair<string, IEnumerable<string>>> headers)
        {
            var tempHeaders = headers.ToList();
            foreach (var header in tempHeaders)
            {
                if (key.ToLower().Equals(header.Key.ToLower()))
                    return (header.Value != null && header.Value.Count() > 0) ? (T)Convert.ChangeType(header.Value.ToList()[0] + string.Empty, typeof(T)) : default(T);
            }

            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="headers"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static T GetHeaderValueByKey<T>(string key, HttpHeaders headers, T defaultValue)
        {
            IEnumerable<string> values;
            headers.TryGetValues(key, out values);
            if (values != null && values.Count() > 0)
                return (T)Convert.ChangeType(values.ToList()[0], typeof(T));

            return defaultValue;
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
