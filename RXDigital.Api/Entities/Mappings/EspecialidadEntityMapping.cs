using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class EspecialidadEntityMapping : IEntityTypeConfiguration<Especialidad>
    {
        public void Configure(EntityTypeBuilder<Especialidad> builder)
        {
            builder.ToTable("especialidad", "admin");
            builder.HasKey(t => t.EspecialidadId);

            builder.Property(t => t.EspecialidadId)
                .HasColumnName("id_especialidad")
                .IsRequired();

            builder.Property(t => t.Descripcion)
                .HasColumnName("descripcion");
        }
    }
}
