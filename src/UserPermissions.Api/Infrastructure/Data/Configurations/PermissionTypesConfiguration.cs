using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserPermissions.Api.Domain.Entities;

namespace UserPermissions.Api.Infrastructure.Data.Configurations
{
    public class PermissionTypesConfiguration : IEntityTypeConfiguration<PermissionType>
    {
        public void Configure(EntityTypeBuilder<PermissionType> builder)
        {
            builder.ToTable("PermissionTypes");

            builder.Property(x => x.Id)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("text");
        }
    }
}
