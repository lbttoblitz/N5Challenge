using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPermissions.Api.Domain.Entities;

namespace UserPermissions.Api.Infrastructure.Data.Configurations
{
    public class PermissionsConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.Property(p => p.Id)
                .IsRequired();

            builder.Property(p => p.EmployeeName)
                .IsRequired()
                .HasColumnType("text");

            builder.Property(p => p.EmployeeLastname)
                .IsRequired()
                .HasColumnType("text");

            builder.HasOne(x => x.PermissionType)
                .WithMany(x => x.Permissions)
                .HasForeignKey(x => x.PermissionTypeId);

            builder.Property(p => p.PermissionDate)
                .HasColumnType("datetime");
        }
    }
}
