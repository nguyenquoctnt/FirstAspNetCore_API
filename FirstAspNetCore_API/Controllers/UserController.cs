using FirstAspNetCore_Help;
using FirstAspNetCore_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstAspNetCore_API.Controllers
{
    [Route(ConstantUtil.API_VERSION_URL + "users")]
    public class UserController : BaseController
    {
        public UserController(IUnitOfWork uow) : base(uow)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("signup", Order = 7)]
        public ResponseModel<UserModel> UserSignup([FromBody]UserModel user)
        {
            
            user.IsNull();
            user.Validate(true);

            var reqHeader = RequestUtil.GetRequestHeader(Request);
            var password = user.Password;
            
            using (var uow = UnitOfWork.Initialize())
            {
                IsNullUOW(uow);
                string message = uow.UserRepository.Add(reqHeader,user);
                if (string.IsNullOrEmpty(message))
                    uow.Commit();
                
                return CreateResponse(message, user);
            }
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}