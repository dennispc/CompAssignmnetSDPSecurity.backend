using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.DataAccess;
using CompAssignmnetSDPSecurity.DataAccess.Entities;
using CompAssignmnetSDPSecurity.DataAccess.Repositories;
using CompAssignmnetSDPSecurity.Domain;
using EntityFrameworkCore.Testing.Moq;
using Xunit;

namespace qwertygroup.DataAccess.Test
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void ProductRepository_IsIProductRepository()
        {
            var fakeContext = Create.MockedDbContextFor<MainDbContext>();
            var repo = new ProductRepository(fakeContext);
            Assert.IsAssignableFrom<IProductRepository>(repo);
        }

        [Fact]
        public void ProductRepository_WithNullDbContext_ThrowsInvalidDataException()
        {
            Assert.Throws<InvalidDataException>(() => new ProductRepository(null));
        }
        
        [Fact]
        public void ProductRepository_WithNullDbContext_ThrowsInvalidDataExceptionWithMessage()
        {
            var exception =  Assert.Throws<InvalidDataException>(() => new ProductRepository(null));
            Assert.Equal("Product repository must have DbContext", exception.Message);
        }

        [Fact]
        public void FindAll_GetAllProductEntitiesInDbContext_AsAListOfProducts()
        {
            var fakeContext = Create.MockedDbContextFor<MainDbContext>();
            var repo = new ProductRepository(fakeContext);
            
            var list = new List<ProductEntity>
            {
                new ProductEntity {Id = 1, Name = "Produkt1"},
                new ProductEntity {Id = 2, Name = "Produkt2"},
                new ProductEntity {Id = 3, Name = "Produkt3"},
                new ProductEntity {Id = 4, Name = "Produkt4"}
            };
            
            fakeContext.Set<ProductEntity>().AddRange(list);
            fakeContext.SaveChanges();

            var expectedResult = list.Select(pe => new Product()
            {
                Id = pe.Id,
                Name = pe.Name
            }).ToList();

            var actualResult = repo.FindAll();
            Assert.Equal(expectedResult, actualResult, new Comparer());
        }
    }

    public class Comparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.Name == y.Name;
        }

        public int GetHashCode(Product obj)
        {
            return HashCode.Combine(obj.Id, obj.Name);
        }
    }
}