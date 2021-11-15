

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using CompAssignmnetSDPSecurity.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace quertygroup.WebApi.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void ProductController_IsOfTypeControllerBase()
        {
            var service = new Mock<IProductService>();
            var controller = new ProductController(service.Object);
            Assert.IsAssignableFrom<ControllerBase>(controller);
        }

        [Fact]
        public void ProductController_UsesApiControllerAttribute()
        {
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("ApiControllerAttribute"));
            Assert.NotNull(attr);
        }

        [Fact]
        public void ProductController_UsesRouteAttribute_WithParamApiControllerNameRoute()
        {
            var typeInfo = typeof(ProductController).GetTypeInfo();
            var attr = typeInfo.GetCustomAttributes()
                .FirstOrDefault(a => a.GetType().Name.Equals("RouteAttribute"));
            
            Assert.NotNull(attr);
            var routeAttr = attr as RouteAttribute;
            Assert.Equal("api/[controller]", routeAttr.Template);
        }

        [Fact]
        public void ProductController_HasGetAllMethod()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.NotNull(method);
        }
        
        [Fact]
        public void ProductController_HasGetAllMethod_IsPublic()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.True(method.IsPublic);
        }
        
        [Fact]
        public void ProductController_HasGetAllMethod_ReturnsListOfProductsInActionResult()
        {
            var method = typeof(ProductController)
                .GetMethods().FirstOrDefault(m => "GetAll".Equals(m.Name));
            Assert.Equal(typeof(ActionResult<List<Product>>).FullName, method.ReturnType.FullName);
        }

        [Fact]
        public void GetAll_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(ProductController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "GetAll");

            var attr = methodInfo.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            
            Assert.NotNull(attr);
        }

        [Fact]
        public void ProductController_HasProductService_IsOfTypeControllerBare()
        {
            
        }
        
        [Fact]
        public void ProductController_WithNullProductService_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() =>
                new ProductController(null));
        }
        
        [Fact]
        public void ProductController_WithNullProductService_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() =>
                new ProductController(null));
            Assert.Equal("ProductService cannot be null", exception.Message);
        }
    }
}