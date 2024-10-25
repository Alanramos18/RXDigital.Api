using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class PrescriptionEntityMapping : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("receta", "admin");
            builder.HasKey(t => t.PrescriptionId);

            builder.Property(t => t.PrescriptionId)
                .HasColumnName("id_receta")
                .IsRequired();

            builder.Property(t => t.Diagnostic)
                .HasColumnName("diagnostico");

            builder.Property(t => t.Indications)
                .HasColumnName("indicaciones");
            
            builder.Property(t => t.Expiration)
                .HasColumnName("expiracion")
                .IsRequired();

            builder.Property(t => t.MedicineId)
                .HasColumnName("id_medicamento")
                .IsRequired();

            builder.Property(t => t.PatientId)
                .HasColumnName("dni_paciente")
                .IsRequired();

            builder.Property(t => t.RegistrationId)
                .HasColumnName("matricula")
                .IsRequired();

            builder.Property(t => t.PharmaceuticalRegistrationId)
                .HasColumnName("matricula_farmaceutico")
                .IsRequired();

            builder.Property(t => t.StatusId)
                .HasColumnName("id_estado")
                .IsRequired();

            builder.HasOne(t => t.Medicine)
                .WithMany(t => t.Prescriptions)
                .HasForeignKey(t => t.MedicineId)
                .IsRequired();

            builder.HasOne(t => t.Patient)
                .WithMany(t => t.Prescriptions)
                .HasForeignKey(t => t.PatientId)
                .IsRequired();

            builder.HasOne(t => t.Doctor)
                .WithMany(t => t.Prescriptions)
                .HasForeignKey(t => t.RegistrationId)
                .IsRequired();

            builder.HasOne(t => t.Pharmaceutical)
                .WithMany(t => t.Prescriptions)
                .HasForeignKey(t => t.PharmaceuticalRegistrationId);

            builder.HasOne(t => t.Status)
                .WithMany(t => t.Prescriptions)
                .HasForeignKey(t => t.StatusId)
                .IsRequired();
        }
    }
}
