using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class PharmaceuticalEntityMapping : IEntityTypeConfiguration<Pharmaceutical>
    {
        public void Configure(EntityTypeBuilder<Pharmaceutical> builder)
        {
            builder.ToTable("farmaceutico", "admin");
            builder.HasKey(t => t.RegistrationId);

            builder.Property(t => t.RegistrationId)
                .HasColumnName("matricula")
                .IsRequired();

            builder.HasOne(t => t.Account)
                .WithOne(t => t.Pharmaceutical)
                .HasForeignKey<Pharmaceutical>(t => t.AccountId)
                .IsRequired();
        }
    }
}
