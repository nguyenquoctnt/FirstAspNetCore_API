using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using FirstAspNetCore_Model;

namespace FirstAspNetCore_API
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other scoped dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var uow = context.RequestServices.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            var content = new ResponseModel<string>()
            {
                Status = false
            };

            if (exception is InvalidArgumentQueryException)
            {
                content.Message = $"Invalid argument query params: { exception.Message }.";
                content.StatusCode = ResponseStatusCode.INVALID_ARGUMENTS;
            }
            else if (exception is RequiredArgumentBodyException)
            {
                content.Message = $"Required argument params: { exception.Message }.";
                content.StatusCode = ResponseStatusCode.REQUIRED_ARGUMENTS;
            }
            else if (exception is InvalidApiException)
            {
                content.Message = $"Some error occurred: { exception.Message }.";
                content.StatusCode = ResponseStatusCode.API_ACCESS_DENIDED;
            }
            else if (exception is PermissionUserException)
            {
                content.Message = $"Access is denied: { exception.Message }.";
                content.StatusCode = ResponseStatusCode.API_PERMISSION_DENIED;
            }
            else if (exception is NotFoundException)
            {
                content.Message = $"Not found: { exception.Message }.";
                content.StatusCode = ResponseStatusCode.NOT_FOUND;
            }
            else
            {
                content.Message = $"Internal server error: { exception.Message }.";
                content.StatusCode = ResponseStatusCode.INTERNAL_SERVER;
            }

            var result = JsonConvert.SerializeObject(content);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 200;
            return context.Response.WriteAsync(result);
        }
    }
}
