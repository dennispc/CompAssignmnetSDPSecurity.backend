using CompAssignmnetSDPSecurity.Core.Models;
using Xunit;

namespace CompAssignmnetSDPSecurity.Core.Test
{
    public class ProductTest
    {
        private Product _product;
        public ProductTest()
        {
            _product = new Product();
        }
        
        [Fact]
        public void Product_CanBeInitialized()
        {
            var product = new Product();
            Assert.NotNull(product);
        }

        [Fact]
        public void Product_Id_MustBeInt()
        {
            Assert.True(_product.Id is int);
        }

        [Fact]
        public void Product_SetId_StoresId()
        {
            var product = new Product();
            product.Id = 1;
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public void Product_SetName_StoresNameAsString()
        {
            var product = new Product();
            product.Name = "Produkt";
            Assert.Equal("Produkt", product.Name);
        }
    }
    
}