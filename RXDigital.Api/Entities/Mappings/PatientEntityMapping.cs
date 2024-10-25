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

            builder.Property(t => t.Gender)
                .HasColumnName("sexo");

            builder.Property(t => t.Nationality)
                .HasColumnName("nacionalidad");

            builder.Property(t => t.Cellphone)
                .HasColumnName("celular");

            builder.Property(t => t.HomePhone)
                .HasColumnName("telefono_fijo");

        builder.HasOne(t => t.SocialWork)
                .WithMany(t => t.Patients)
                .HasForeignKey(t => t.SocialWorkId)
                .IsRequired();
        }
    }
}
