using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class PrescriptionMedicineEntityMapping : IEntityTypeConfiguration<PrescriptionMedicine>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicine> builder)
        {
            builder.ToTable("Receta_Medicamento", "admin");
            builder.HasKey(t => new { t.MedicineId, t.PrescriptionCode });

            builder.Property(t => t.MedicineId)
                .HasColumnName("id_medicamento")
                .IsRequired();

            builder.Property(t => t.PrescriptionCode)
                .HasColumnName("codigo_receta")
                .IsRequired();

            builder.Property(t => t.Indications)
                .HasColumnName("indicaciones")
                .IsRequired();

            builder.HasOne(t => t.Medicine)
                .WithMany(t => t.PrescriptionMedicines)
                .HasForeignKey(t => t.MedicineId)
                .IsRequired();

            builder.HasOne(t => t.Prescription)
                .WithMany(t => t.PrescriptionMedicines)
                .HasForeignKey(t => t.PrescriptionCode)
                .IsRequired();
        }
    }
}
