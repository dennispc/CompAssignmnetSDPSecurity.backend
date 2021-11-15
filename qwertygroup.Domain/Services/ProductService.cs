using System.Collections.Generic;
using System.IO;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;

namespace CompAssignmnetSDPSecurity.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new InvalidDataException("ProductRepository can not be null");
        }

        public List<Product> GetProducts()
        {
            return _productRepository.FindAll();
        }
    }
}