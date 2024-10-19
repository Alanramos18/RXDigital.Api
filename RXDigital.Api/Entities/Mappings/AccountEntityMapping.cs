using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class AccountEntityMapping : IEntityTypeConfiguration<AccountEntity>
    {
        public void Configure(EntityTypeBuilder<AccountEntity> builder)
        {
            builder.ToTable("Usuario", "admin");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id_usuario")
                .IsRequired();

            builder.Property(t => t.FirstName)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(t => t.LastName)
                .HasColumnName("apellido")
                .IsRequired();

            builder.Property(t => t.Email)
                .HasColumnName("email")
                .IsRequired();

            builder.Property(t => t.PasswordHash)
                .HasColumnName("contraseña")
                .IsRequired();

            builder.Property(t => t.RoleId)
                .HasColumnName("id_rol");

            builder.HasOne(t => t.Role)
                .WithMany(t => t.Accounts)
                .HasForeignKey(t => t.RoleId)
                .IsRequired();

            //builder.Ignore(t => t.UserName);
            //builder.Ignore(t => t.NormalizedUserName);
        }
    }
}
