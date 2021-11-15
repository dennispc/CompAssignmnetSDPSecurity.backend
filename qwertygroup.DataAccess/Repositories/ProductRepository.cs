using System.Collections.Generic;
using System.IO;
using System.Linq;
using CompAssignmnetSDPSecurity.Core.Models;
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
    }
}