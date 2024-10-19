using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class StatusEntityMapping : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("estado", "admin");
            builder.HasKey(t => t.StatusId);

            builder.Property(t => t.StatusId)
                .HasColumnName("id_estado")
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("descripcion")
                .IsRequired();
        }
    }
}
