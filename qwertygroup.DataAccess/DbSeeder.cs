using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.DataAccess.Entities;

namespace CompAssignmnetSDPSecurity.DataAccess
{
    public class DbSeeder
    {
        private readonly MainDbContext _context;

        public DbSeeder(MainDbContext context)
        {
            _context = context;
        }

        public void SeedDevelopmentDb()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            _context.Products.Add(new ProductEntity() {Id = 1, Name = "Cheddar"});
            _context.Products.Add(new ProductEntity() {Id = 2, Name = "Brie"});
            _context.Products.Add(new ProductEntity() {Id = 3, Name = "Gouda"});
            _context.Products.Add(new ProductEntity() {Id = 4, Name = "Havarti"});
            _context.Products.Add(new ProductEntity() {Id = 5, Name = "Mozzarella"});
            _context.SaveChanges();
        }
    }
}