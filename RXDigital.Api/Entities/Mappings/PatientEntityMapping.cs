using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class PatientEntityMapping : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("paciente", "admin");
            builder.HasKey(t => t.PatientId);

            builder.Property(t => t.PatientId)
                .HasColumnName("dni")
                .IsRequired();

            builder.Property(t => t.FirstName)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasColumnName("apellido")
                .IsRequired();

            builder.Property(t => t.BirthDay)
                .HasColumnName("fecha_nacimiento")
                .IsRequired();

            builder.Property(t => t.SocialNumber)
                .HasColumnName("numero_afiliado")
                .IsRequired();

            builder.Property(t => t.SocialWorkId)
                .HasColumnName("id_obra_social");

            builder.HasOne(t => t.SocialWork)
                .WithMany(t => t.Patients)
                .HasForeignKey(t => t.SocialWorkId)
                .IsRequired();
        }
    }
}
