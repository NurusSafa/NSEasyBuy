using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSEasyBuy.Service.DTOs
{
    public class ExtApiErrorResponse
    {
        [JsonProperty("apiError")]
        public string ApiErrorMessage;
    }
}
