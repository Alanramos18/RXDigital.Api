using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class PatientEntityMapping : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("paciente", "admin");
            builder.HasKey(t => t.Dni);

            builder.Property(t => t.Dni)
                .HasColumnName("dni")
                .IsRequired();

            builder.Property(t => t.Nombre)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(t => t.Apellido)
                .HasColumnName("apellido")
                .IsRequired();

            builder.Property(t => t.FechaNacimiento)
                .HasColumnName("fecha_nacimiento")
                .IsRequired();

            builder.Property(t => t.FechaInscripcion)
                .HasColumnName("fecha_inscripcion")
                .IsRequired();

            builder.Property(t => t.NumeroAfiliado)
                .HasColumnName("numero_afiliado");

            builder.Property(t => t.SocialWorkId)
                .HasColumnName("id_obra_social");

            builder.Property(t => t.Genero)
                .HasColumnName("sexo");

            builder.Property(t => t.Celular)
                .HasColumnName("celular");

            builder.Property(t => t.Telefono)
                .HasColumnName("telefono_fijo");

            builder.Property(t => t.Habilitado)
                .HasColumnName("habilitado");

            builder.Property(t => t.Domicilio)
                .HasColumnName("domicilio");

            builder.Property(t => t.Localidad)
                .HasColumnName("localidad");

            builder.Property(t => t.Nacionalidad)
                .HasColumnName("nacionalidad");

            builder.Property(t => t.Provincia)
                .HasColumnName("provincia");


            builder.HasOne(t => t.SocialWork)
                .WithMany(t => t.Patients)
                .HasForeignKey(t => t.SocialWorkId)
                .IsRequired();

            builder.HasMany(p => p.Prescriptions)
                .WithOne(t => t.Patient)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
