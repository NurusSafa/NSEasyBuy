using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NSEasyBuy.Service.DTOs
{
    public class BaseResponse
    {
        public enum EResponseType
        {
            Success = 20,
            Created = 21,
            Deleted = 24,
            //
            NotModified = 31,
            //
            BadRequest = 40,
            Unauthorized = 41,
            Forbidden = 43,
            NotFound = 44,
            UnprocessableEntity = 45,
            //
            Exception = 50
        }

        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonIgnore]
        public EResponseType Status { get; set; }
        [JsonProperty("success")]
        public bool IsSuccess
        {
            get { return _getResponseStatus(); }
            private set {; }
        }
        private bool _getResponseStatus()
        {
            bool success = true;
            if (Status == EResponseType.BadRequest || Status == EResponseType.Exception
                || Status == EResponseType.Forbidden || Status == EResponseType.NotFound
                || Status == EResponseType.Unauthorized || Status == EResponseType.UnprocessableEntity)
            {
                success = false;
            }

            return success;
        }

    }

    public class SuccessResponse : BaseResponse
    {
        [JsonProperty("record")]
        public object Record { get; set; }

        public static implicit operator Task<object>(SuccessResponse v)
        {
            throw new NotImplementedException();
        }
    }

    public class ErrorResponse : BaseResponse
    {
        [JsonProperty("errorMessage")]
        public List<string> Description { get; set; }
    }
}
