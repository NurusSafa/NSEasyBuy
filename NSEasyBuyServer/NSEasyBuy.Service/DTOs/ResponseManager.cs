using System;
using System.Collections.Generic;
using System.Text;

namespace NSEasyBuy.Service.DTOs
{
    public static class ResponseManager
    {
        public static BaseResponse ManageResponse(object objResponse)
        {
            BaseResponse response;
            try
            {
                response = new SuccessResponse(); 
                SuccessResponse successResponse = response as SuccessResponse;
                
                if (objResponse == null)
                {
                    successResponse.Message = "";
                }
                else if (objResponse.GetType() == typeof(ExtApiErrorResponse))
                {
                    ExtApiErrorResponse valAPIError = new ExtApiErrorResponse();
                    valAPIError = objResponse as ExtApiErrorResponse;
                    successResponse.Message = "apiError:" + valAPIError.ApiErrorMessage;     
                    successResponse.Status = BaseResponse.EResponseType.Exception;
                }
                else if (objResponse.GetType().IsSubclassOf(typeof(Exception)))
                {
                    Exception ex = successResponse.Record as Exception;
                    response = ManageException(ex);
                }
                else
                {
                    successResponse.Record = objResponse;
                    successResponse.Message = "Record found Successfully";
                    successResponse.Status = BaseResponse.EResponseType.Success;  
                }
                return successResponse;
            }
            catch (Exception ex)
            {
                response = ManageException(ex);
            }
            return response;
        }

        private static BaseResponse ManageException(Exception ex)
        {
            BaseResponse response = new ErrorResponse();
            ErrorResponse errorResponse = response as ErrorResponse;
            errorResponse.Message = "Server Problem";
            errorResponse.Description = new List<string>();
            errorResponse.Description.Add(ex.Message);
            errorResponse.Description.Add(ex.StackTrace.ToString());
            errorResponse.Status = BaseResponse.EResponseType.Exception;
            return response;
        }
    }
}
