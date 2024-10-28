using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class LocationEntityMapping : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.ToTable("localidad", "admin");
            builder.HasKey(t => t.LocationId);

            builder.Property(t => t.LocationId)
                .HasColumnName("id_localidad")
                .IsRequired();

            builder.Property(t => t.Name)
                .HasColumnName("nombre")
                .IsRequired();

            builder.Property(t => t.Province)
                .HasColumnName("provincia")
                .IsRequired();

            builder.Property(t => t.Country)
                .HasColumnName("pais")
                .IsRequired();
        }
    }
}
