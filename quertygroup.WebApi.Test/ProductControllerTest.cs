

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
    }
}