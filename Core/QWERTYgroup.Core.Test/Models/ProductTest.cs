using QWERTYgroup.Core.Models;
using Xunit;
namespace QWERTYgroup.Core.Test
{
    public class ProductTest
    {
        public Product product = new Product();

        [Fact]
        public void ProductExistTest(){
            Assert.NotNull(product);
        }

        [Fact]
        public void IdTest(){
            int value = 1;
            product.Id=value;
            Assert.Equal(product.Id,value);
        }
    
        [Fact]
        public void NameTest(){
            string value = "Somename";
            product.Name=value;
            Assert.Equal(product.Name,value);
        }
    }
}