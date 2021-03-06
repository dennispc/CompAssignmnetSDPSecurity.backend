using CompAssignmnetSDPSecurity.Security.Models;
using Microsoft.EntityFrameworkCore;

namespace CompAssignmnetSDPSecurity.Security
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options){}    

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<UserPermission>()
            .HasKey(u=>new {u.PermissionId, u.UserId});
        }

        public DbSet<Permission> Permissions{get;set;}
        public DbSet<LoginUser> LoginUsers{get;set;}
        public DbSet<UserPermission> UserPermissions{get;set;}
    }
}