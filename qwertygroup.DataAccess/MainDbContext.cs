using CompAssignmnetSDPSecurity.Core.Models;
using CompAssignmnetSDPSecurity.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompAssignmnetSDPSecurity.DataAccess
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
            
        }

        public virtual DbSet<ProductEntity> Products { get; set; }
    }
}