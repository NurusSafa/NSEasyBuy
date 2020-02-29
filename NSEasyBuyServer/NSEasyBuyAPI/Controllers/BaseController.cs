using System.Linq;
using Microsoft.AspNetCore.Mvc;
using NSEasyBuy.Service.DTOs;
using static NSEasyBuy.Service.DTOs.BaseResponse;

namespace BN.Api.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult GetHttpResponse(BaseResponse responseObject) {
            IActionResult actionResult;
            switch (responseObject.Status) {
                case EResponseType.Success:
                    actionResult = Ok(responseObject);
                    break;
                case EResponseType.BadRequest:
                    actionResult = new BadRequestObjectResult(responseObject);
                    break;
                case EResponseType.Exception:
                    actionResult = new ObjectResult(responseObject);
                    break;
                case EResponseType.NotFound:
                    actionResult = new ObjectResult(responseObject);
                    break;
                case EResponseType.Unauthorized:
                    actionResult = new StatusCodeResult(401);
                    break;
                case EResponseType.Forbidden:
                    actionResult = new StatusCodeResult(403);
                    break;
                default:
                    actionResult = new ObjectResult(responseObject);
                    break;
            }
            return actionResult;
        }

        protected IActionResult GetCustomResponse(BaseResponse responseObject) {
            IActionResult actionResult = Ok(responseObject);
            return actionResult;
        }

        protected BaseResponse GetBadRequestResponse(string customMessage) {
            BaseResponse response = new ErrorResponse();
            response.Status = EResponseType.BadRequest;
            response.Message = customMessage.Trim().Length > 0 ? customMessage : ResponseMessage.BAD_REQUEST;
            ((ErrorResponse)response).Description = ModelState.Values.SelectMany(v => v.Errors).Select(modelError => modelError.ErrorMessage).ToList();

            return response;
        }

    }
}