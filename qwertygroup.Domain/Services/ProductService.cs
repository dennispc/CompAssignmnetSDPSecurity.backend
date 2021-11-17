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

        public Product GetProduct(int id)
        {
            return _productRepository.ReadById(id);
        }

        public bool DeleteProduct(Product product)
        {
            return _productRepository.Delete(product);
        }

        public Product UpdateProduct(Product product)
        {
            return _productRepository.Update(product);
        }

        public Product CreateProduct(Product product)
        {
            return _productRepository.Create(product);
        }
    }
}