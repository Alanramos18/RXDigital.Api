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

            builder.Property(t => t.AccountId)
                .HasColumnName("id_usuario");

            builder.HasOne(t => t.Account)
                .WithOne(t => t.Doctor)
                .HasForeignKey<Doctor>(t => t.AccountId)
                .IsRequired();
        }
    }
}
