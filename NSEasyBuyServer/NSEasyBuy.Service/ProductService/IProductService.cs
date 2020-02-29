using NSEasyBuy.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSEasyBuy.Service.ProductService
{
    public interface IProductService
    {
        public BaseResponse GetProductsByKeywords(string strText);
    }
}
