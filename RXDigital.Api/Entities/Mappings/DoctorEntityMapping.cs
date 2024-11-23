using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class DoctorEntityMapping : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("medico", "admin");
            builder.HasKey(t => t.RegistrationId);

            builder.Property(t => t.RegistrationId)
                .HasColumnName("matricula")
                .IsRequired();

            builder.Property(t => t.UserId)
                .HasColumnName("id_usuario");

            builder.Property(t => t.EspecialidadId)
                .HasColumnName("id_especialidad");

            builder.HasOne(t => t.User)
                .WithOne(t => t.Doctor)
                .HasForeignKey<Doctor>(t => t.UserId)
                .IsRequired();

            builder.HasOne(t => t.Especialidad)
                .WithOne(t => t.Doctor)
                .HasForeignKey<Doctor>(t => t.EspecialidadId)
                .IsRequired();
        }
    }
}
