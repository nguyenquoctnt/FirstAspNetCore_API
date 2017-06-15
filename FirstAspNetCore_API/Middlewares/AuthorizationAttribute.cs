using AutoSpy_Help;
using AutoSpy_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace FirstAspNetCore_API.Middlewares
{
    public class AuthorizationRequirement : IAuthorizationRequirement
    {
        public AuthorizationRequirement(IUnitOfWork uow, IConfiguration appSetting)
        {
            UnitOfWork = uow;
            AppSetting = appSetting;
        }
        public IUnitOfWork UnitOfWork { get; set; }
        public IConfiguration AppSetting { get; set; }
    }

    public class AuthorizationHandler : AuthorizationHandler<AuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthorizationRequirement requirement)
        {
            try
            {
                var secretKey = requirement.AppSetting.GetValue<string>("SecretKey", string.Empty);
                if (!string.IsNullOrEmpty(secretKey))
                {
                    var ctx = context.Resource as AuthorizationFilterContext;
                    var reqHeader = RequestUtil.GetRequestHeader(ctx.HttpContext.Request);
                    var tokenData = Crypto.Decrypt<UserAccessTokenViewModel>(reqHeader.AccessToken, secretKey);
                    if (tokenData != null)
                    {
                        var utcTime = DateTime.UtcNow;
                        if (utcTime < tokenData.ExpiredTime.ToDateTime())
                        {
                            var unitOfWork = ctx.HttpContext.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
                            using (var uow = unitOfWork.Initialize())
                            {
                                var sUser = uow.UserRepository.Find(reqHeader, new UserModel { Email = tokenData.Username });
                                if (sUser != null && !string.IsNullOrEmpty(sUser.Email) && !string.IsNullOrEmpty(sUser.Password))
                                {
                                    if (sUser.Password.Equals(tokenData.Password))
                                    {
                                        if (sUser.Active ?? false)
                                        {
                                            if (!(sUser.Locked ?? false))
                                            {
                                                ctx.HttpContext.Request.Headers.Add("user_id", new Microsoft.Extensions.Primitives.StringValues(sUser.UserId + string.Empty));
                                                ctx.HttpContext.Request.Headers.Add("user_type_id", new Microsoft.Extensions.Primitives.StringValues((int)sUser.UserType + string.Empty));
                                                ctx.HttpContext.Request.Headers.Add("user_password", new Microsoft.Extensions.Primitives.StringValues(sUser.Password));

                                                context.Succeed(requirement);
                                                return Task.CompletedTask;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                context.Fail();
            }
            catch (Exception ex)
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
