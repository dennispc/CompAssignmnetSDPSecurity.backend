

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.Core.Services;
using CompAssignmnetSDPSecurity.Domain;
using CompAssignmnetSDPSecurity.Domain.Services;
using CompAssignmnetSDPSecurity.WebApi.Controllers;
using CompAssignmnetSDPSecurity.WebApi.Dtos;
using CompAssignmnetSDPSecurity.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace quertygroup.WebApi.Test
{
    public class ProductControllerTest
    {
        private Mock<IProductService> _service;
        private ProductController _controller;
        private Mock<IMapper> _mapper;

        public ProductControllerTest()
        {
            _mapper = new Mock<IMapper>();
            _service = new Mock<IProductService>();
            _controller = new ProductController(_service.Object, _mapper.Object);
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
            Assert.Equal(typeof(ActionResult<List<ProductDto>>).FullName, method.ReturnType.FullName);
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
        public void GetProduct_WithNoParams_HasGetHttpAttribute()
        {
            var methodInfo = typeof(ProductController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "GetProduct");

            var attr = methodInfo.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpGetAttribute");
            
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void GetProduct_IdIsZero_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() =>
                _controller.GetProduct(0));
        }

        [Fact]
        public void GetProduct_IdIsZero_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() =>
                _controller.GetProduct(0));
            Assert.Equal("Id must be greater than zero", exception.Message);
        }

        [Fact]
        public void GetProduct_CallsProductServiceGetProduct_ExactlyOnce()
        {
            _controller.GetProduct(1);
            _service.Verify(r => r.GetProduct(1), Times.Once);
        }
        
        //
        
        [Fact]
        public void DeleteProduct_WithNoParams_HasDeleteHttpAttribute()
        {
            var methodInfo = typeof(ProductController)
                .GetMethods()
                .FirstOrDefault(m => m.Name == "DeleteProduct");

            var attr = methodInfo.CustomAttributes
                .FirstOrDefault(ca => ca.AttributeType.Name == "HttpDeleteAttribute");
            
            Assert.NotNull(attr);
        }
        
        [Fact]
        public void DeleteProduct_ProductDtoIsNull_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() =>
                _controller.DeleteProduct(null));
        }

        [Fact]
        public void DeleteProduct_IdIsZero_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() =>
                _controller.DeleteProduct(null));
            Assert.Equal("Invalid product", exception.Message);
        }

        //

        [Fact]
        public void ProductController_HasProductService_IsOfTypeControllerBase()
        {
            Assert.IsAssignableFrom<ControllerBase>(_controller);
        }
        
        [Fact]
        public void ProductController_WithNullProductServiceAndMapper_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() =>
                new ProductController(null, null));
        }
        
        [Fact]
        public void ProductController_WithNullProductServiceAndMapper_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception = Assert.Throws<InvalidDataException>(() =>
                new ProductController(null, null));
            Assert.Equal("ProductService and IMapper cannot be null", exception.Message);
        }
    }
}