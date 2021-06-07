using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class PostRateConfig : IEntityTypeConfiguration<PostRate>
    {
        public void Configure(EntityTypeBuilder<PostRate> builder)
        {
            builder
                .ToTable("PostRate");

            builder
                .Property(e => e.PostRateId)
                .ValueGeneratedOnAdd();
                
            builder
                .HasIndex(e => e.PostId, "IX_PostRate_PostId");
                
            builder
                .HasOne(d => d.Post)
                .WithMany(p => p.PostRates)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_PostRate_PostId");
            
            builder
                .Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(450);
        }
    }
}