using System.Collections.Generic;
using System.IO;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CompAssignmnetSDPSecurity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            if (productService == null)
            {
                throw new InvalidDataException("ProductService cannot be null");
            }
            _productService = productService;
        }
        
        [HttpGet]
        public ActionResult<List<Product>> GetAll()
        {
            return null;
        }

    }
}