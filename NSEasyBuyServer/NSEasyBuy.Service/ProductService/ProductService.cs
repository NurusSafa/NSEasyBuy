using NSEasyBuy.BLL;
using NSEasyBuy.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSEasyBuy.Service.ProductService
{
    public class ProductService : IProductService
    {
        private ProductManager _ProductManager; 
        public ProductService(ProductManager productManager)
        {
            _ProductManager = productManager;
           
        }
        public BaseResponse GetProductsByKeywords(string strText)
        {
            _ProductManager._MethodName = ProductManager.enumMethod.productsbyKey;
            return ResponseManager.ManageResponse(_ProductManager._PoductDataHandler(strText));
        }
    }
}
