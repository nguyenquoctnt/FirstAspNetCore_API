using Microsoft.AspNetCore.Mvc;
using FirstAspNetCore_Model;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace FirstAspNetCore_API.Controllers
{
    //[Authorize(Policy = "Authorization")]
    [Produces("application/json")]
    public class BaseController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get { return _unitOfWork; }
            set { _unitOfWork = value; }
        }
        
        public BaseController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public static void IsNullUOW(IUnitOfWork uow)
        {
            if (uow == null)
                throw new NotFoundException("connection");
        }

        public static ResponseModel<string> CreateResponse(string message)
        {
            return new ResponseModel<string>(message);
        }

        public static ResponseModel<T> CreateResponse<T>(T data)
        {
            return new ResponseModel<T>(data);
        }

        public static ResponseModel<T> CreateResponse<T>(T data, int totalRow)
        {
            var response = CreateResponse(data);
            response.Total = totalRow;

            return response;
        }

        public static ResponseModel<T> CreateResponse<T>(T data, bool status)
        {
            return new ResponseModel<T>(data, status);
        }

        public static ResponseModel<T> CreateResponse<T>(string message, T data)
        {
            return new ResponseModel<T>(message, data);
        }

        public static ResponseModel<T> CreateResponse<T>(T data, bool status, ResponseStatusCode statusCode)
        {
            return new ResponseModel<T>(data, status, statusCode);
        }

        public static ResponseModel<T> CreateResponse<T>(string message, T data, ResponseStatusCode statusCode)
        {
            return new ResponseModel<T>(message, data, statusCode);
        }
    }
}