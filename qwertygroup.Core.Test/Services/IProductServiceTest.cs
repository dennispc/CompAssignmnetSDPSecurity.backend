using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using Moq;
using Xunit;

namespace CompAssignmnetSDPSecurity.Core.Test.Services
{
    public class IProductServiceTest
    {
        [Fact]
        public void IProductService_IsAvailable()
        {
            var service = new Mock<IProductService>().Object;
            Assert.NotNull(service);
        }

        [Fact]
        public void GetProducts_WithNoParams_ReturnsListOfAllProducts()
        {
            var mock = new Mock<IProductService>();
            var fakeList = new List<Product>();
            mock.Setup(s => s.GetProducts())
                .Returns(new List<Product>());

            var service = mock.Object;
            Assert.Equal(fakeList, service.GetProducts());
        }
    }
}