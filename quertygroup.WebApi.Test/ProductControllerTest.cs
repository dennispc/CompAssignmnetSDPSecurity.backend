

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace quertygroup.WebApi.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void ProductController_IsOfTypeControllerBare()
        {
            var controller = new ProductController();
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
    }
}