using Application.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Identity.Configurations
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder
                .ToTable("RefreshToken");

            builder
                .Property(e => e.RefreshTokenId)
                .ValueGeneratedOnAdd();

            builder
                .Property(e => e.Token)
                .IsRequired()
                .HasMaxLength(100);
            
            builder
                .Property(e => e.CreatedAt)
                .IsRequired();
            
            builder
                .Property(e => e.ExpiresAt)
                .IsRequired();
        }
    }
}