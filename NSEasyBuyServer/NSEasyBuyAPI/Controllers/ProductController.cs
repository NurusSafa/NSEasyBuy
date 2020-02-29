using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BN.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSEasyBuy.Service.ProductService;

namespace NSEasyBuyAPI.Controllers
{
    [Route("api/Product")]
    //[ApiController]
    [Produces("application/json")]
    public class ProductController : BaseController
    {
        private readonly IProductService _ProductService;

        public ProductController(IProductService productService)
        {
            _ProductService = productService;
        }
        //[Route("GetProducts")]
        [HttpGet("{keywords}")]
        [AllowAnonymous]
        public IActionResult GetProduct(string keywords)
        {
            return GetHttpResponse(_ProductService.GetProductsByKeywords(keywords));
        }
    }
}