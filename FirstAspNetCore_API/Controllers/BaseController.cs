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
        
        public static ResponseModel<T> CreateResponse<T>(string message, T data)
        {
            return new ResponseModel<T>(message, data);
        }
        
    }
}