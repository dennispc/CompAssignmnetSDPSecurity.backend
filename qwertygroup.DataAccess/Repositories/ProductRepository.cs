using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.DataAccess.Entities;
using CompAssignmnetSDPSecurity.Domain;

namespace CompAssignmnetSDPSecurity.DataAccess.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MainDbContext _context;

        public ProductRepository(MainDbContext context)
        {
            if (context == null)
            {
                throw new InvalidDataException("Product repository must have DbContext");
            }
            _context = context;
        }

        public List<Product> FindAll()
        {
            return _context.Products.Select(pe => new Product()
            {
                Id = pe.Id,
                Name = pe.Name
            }).ToList();
        }

        public bool Delete(Product product)
        {
            var result = _context.Products.Remove(new ProductEntity()
            {
                Id = product.Id,
                Name = product.Name
            });
            _context.SaveChanges();

            return result != null;
        }

        public Product Create(Product product)
        {
            var entity = new ProductEntity()
            {
                Name = product.Name
            };

            var petEntity = _context.Products.Add(entity).Entity;

            _context.SaveChanges();

            return ReadById(petEntity.Id);
        }

        public Product ReadById(int id)
        {
            var productEntity = _context.Products.FirstOrDefault(p => p.Id == id);
            return new Product()
            {
                Id = productEntity.Id,
                Name = productEntity.Name
            };
        }

        public Product Update(Product product)
        {
            var productDb = _context.Update(product).Entity;
            _context.SaveChanges();

            return new Product()
            {
                Id = productDb.Id,
                Name = productDb.Name
            };
        }
    }
}