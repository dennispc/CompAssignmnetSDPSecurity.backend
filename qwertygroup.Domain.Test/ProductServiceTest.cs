using CompAssignmnetSDPSecurity.Core.Services;
using Xunit;

namespace qwertygroup.Domain.Test
{
    public class ProductServiceTest
    {
        [Fact]
        public void ProductService_IsIProductService()
        {
            var service = new ProductService();
            Assert.True(service is IProductService);
        }
    }
}