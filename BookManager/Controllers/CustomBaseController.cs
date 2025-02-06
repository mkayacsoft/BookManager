using System.Net;
using BookManager.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult.HttpStatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.HttpStatusCode.GetHashCode() };
            }

            return new ObjectResult(serviceResult) { StatusCode = serviceResult.HttpStatusCode.GetHashCode() };
        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult                                                                                                                                                                                              serviceResult)
        {
            if (serviceResult.HttpStatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.HttpStatusCode.GetHashCode() };
            }

            return new ObjectResult(serviceResult) { StatusCode = serviceResult.HttpStatusCode.GetHashCode() };
        }
    }
}
