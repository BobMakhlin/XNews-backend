using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .ToTable("Comment");

            builder
                .Property(e => e.CommentId)
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(e => e.ParentCommentId, "IX_Comment_ParentCommentId");

            builder
                .HasIndex(e => e.PostId, "IX_Comment_PostId");

            builder
                .Property(e => e.Content)
                .IsRequired()
                .HasMaxLength(320);

            builder
                .HasOne(d => d.ParentComment)
                .WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_Comment_ParentComment");
        }
    }
}