using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserPermissions.Api.Domain.Entities;

namespace UserPermissions.Api.Infrastructure.Data
{
    public class PermissionContext : DbContext
    {
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }

        public PermissionContext()
        {
        }
        public PermissionContext(DbContextOptions<PermissionContext> options)
             : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
