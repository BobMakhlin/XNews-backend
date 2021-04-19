using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class PostConfig : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .ToTable("Post");

            builder
                .Property(e => e.PostId)
                .HasDefaultValueSql("NEWID()");

            builder
                .Property(e => e.Content)
                .IsRequired();

            builder
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(72);
        }
    }
}