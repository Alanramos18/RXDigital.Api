using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class MedicineEntityMapping : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("medicina", "admin");
            builder.HasKey(t => t.MedicineId);

            builder.Property(t => t.MedicineId)
                .HasColumnName("id_medicamento")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("nombre_comercial")
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("descripcion")
                .IsRequired();
        }
    }
}
