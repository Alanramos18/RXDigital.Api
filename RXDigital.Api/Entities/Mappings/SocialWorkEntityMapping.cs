using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RXDigital.Api.Entities.Mappings
{
    internal class SocialWorkEntityMapping : IEntityTypeConfiguration<SocialWork>
    {
        public void Configure(EntityTypeBuilder<SocialWork> builder)
        {
            builder.ToTable("obrasocial", "admin");
            builder.HasKey(t => t.SocialWorkId);

            builder.Property(t => t.SocialWorkId)
                .HasColumnName("id_obra_social")
                .IsRequired();

            builder.Property(t => t.SocialPlan)
                .HasColumnName("plan_social")
                .IsRequired();
        }
    }
}
