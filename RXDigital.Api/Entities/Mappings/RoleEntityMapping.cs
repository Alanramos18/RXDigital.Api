using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class RoleEntityMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("rol", "admin");
            builder.HasKey(t => t.RoleId);

            builder.Property(t => t.RoleId)
                .HasColumnName("id_rol")
                .IsRequired();

            builder.Property(t => t.Description)
                .   HasColumnName("descripcion")
                .IsRequired();
        }
    }
}
