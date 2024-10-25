using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class MedicineEntityMapping : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            builder.ToTable("medicamento", "admin");
            builder.HasKey(t => t.MedicineId);

            builder.Property(t => t.MedicineId)
                .HasColumnName("id_medicamento")
                .IsRequired();

            builder.Property(t => t.CommercialName)
                .HasColumnName("nombre_comercial")
                .IsRequired();

            builder.Property(t => t.Description)
                .HasColumnName("descripcion")
                .IsRequired();

            builder.Property(t => t.Presentation)
                .HasColumnName("presentacion")
                .IsRequired();

            builder.Property(t => t.Concentration)
                .HasColumnName("concentracion")
                .IsRequired();
        }
    }
}
