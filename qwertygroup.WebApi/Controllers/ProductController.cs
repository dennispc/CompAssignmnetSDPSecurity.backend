using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using CompAssignmnetSDPSecurity.WebApi.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompAssignmnetSDPSecurity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService,
            IMapper mapper)
        {
            if (productService == null)
            {
                throw new InvalidDataException("ProductService and IMapper cannot be null");
            }
            _productService = productService;
            _mapper = mapper;
        }
        
        [HttpGet]
        public ActionResult<List<ProductDto>> GetAll()
        {
            return _productService.GetProducts().Select(p => new ProductDto()
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }
        
        [HttpGet("id")]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            if (id <= 0)
            {
                throw new InvalidDataException("Id must be greater than zero");
            }

            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return BadRequest("No product was found with id: " + id);
            }

            return _mapper.Map<Product, ProductDto>(product);
        }
        
        [HttpPost]
        public ActionResult<ProductDto> AddProduct([FromBody] ProductDto product)
        {
            if (_productService.GetProducts().Any(p => p.Name.ToLower().Equals(product.Name.ToLower())))
            {
                throw new InvalidDataException("A product with this name exist");
            }

            var prod = _productService.CreateProduct(new Product()
            {
                Name = product.Name
            });

            if (prod == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return _mapper.Map<Product, ProductDto>(prod);
        }

        [HttpPut("id")]
        public ActionResult<ProductDto> UpdateProduct(int id, [FromBody] Product productDto)
        {
            try
            {
                if (id != productDto.Id)
                {
                    return BadRequest("Id not matching");
                }

                var product = _productService.UpdateProduct(new Product()
                {
                    Id = productDto.Id,
                    Name = productDto.Name
                });

                return Ok(_mapper.Map<Product, ProductDto>(product));
            }
            catch (Exception e)
            {
                return StatusCode(500, "Server error occured when attempting to update product");
            }
        }

        [HttpDelete]
        public ActionResult DeleteProduct([FromBody] ProductDto productDto)
        {
            if (productDto == null)
            {
                throw new InvalidDataException("Invalid product");
            }

            return Ok(_productService.DeleteProduct(new Product()
            {
                Id = productDto.Id,
                Name = productDto.Name
            }));
        }

    }
}