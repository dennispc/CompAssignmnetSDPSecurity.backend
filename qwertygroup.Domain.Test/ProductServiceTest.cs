using System.Collections.Generic;
using System.IO;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using CompAssignmnetSDPSecurity.Domain;
using CompAssignmnetSDPSecurity.Domain.Services;
using Moq;
using Xunit;

namespace qwertygroup.Domain.Test
{
    public class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _mock;
        private readonly ProductService _service;

        public ProductServiceTest()
        {
            _mock = new Mock<IProductRepository>();
            _service = new ProductService(_mock.Object);
        }
        
        [Fact]
        public void ProductService_IsIProductService()
        {
            Assert.True(_service is IProductService);
        }

        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ProductService(null));
        }
        
        [Fact]
        public void ProductService_WithNullProductRepository_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() => new ProductService(null));
            
            Assert.Equal("ProductRepository can not be null", exception.Message);
        }

        [Fact]
        public void GetProducts_CallsProductRepositoryFindAll_ExactlyOnce()
        {
            _service.GetProducts();
            _mock.Verify(r => r.FindAll(), Times.Once);
        }

        [Fact]
        public void GetProducts_NoFilter_ReturnsListOfAllProducts()
        {
            var list = new List<Product>()
            {
                new Product {Id = 1, Name = "Produkt1"},
                new Product {Id = 2, Name = "Produkt2"}
            };
            
            _mock.Setup(r => r.FindAll())
                .Returns(list);
            Assert.Equal(list, _service.GetProducts());
        }
    }
}