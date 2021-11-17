using System.Collections.Generic;
using System.IO;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using CompAssignmnetSDPSecurity.WebApi.Dtos;
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
        
        [HttpGet("id")]
        public ActionResult<List<Product>> GetProduct(int id)
        {
            return null;
        }
        
        [HttpPut]
        public ActionResult<List<Product>> AddProduct(ProductDto product)
        {
            return null;
        }
        // TODO put
        // TODO update
        // TODO delete

    }
}