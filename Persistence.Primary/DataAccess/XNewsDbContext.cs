using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Primary.DataAccess
{
    public sealed class XNewsDbContext : DbContext, IXNewsDbContext
    {
        #region Constructors

        public XNewsDbContext(DbContextOptions<XNewsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion

        #region Properties

        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentRate> CommentRate { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostRate> PostRate { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity
                    .Property(e => e.CategoryId)
                    .HasDefaultValueSql("NEWID()");
                
                entity
                    .HasIndex(e => e.Title, "UQ_Category")
                    .IsUnique();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(32);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");
                
                entity
                    .Property(e => e.CommentId)
                    .HasDefaultValueSql("NEWID()");

                entity.HasIndex(e => e.ParentCommentId, "IX_Comment_ParentCommentId");

                entity.HasIndex(e => e.PostId, "IX_Comment_PostId");

                entity
                    .Property(e => e.Content)
                    .IsRequired()
                    .HasMaxLength(320);

                entity
                    .HasOne(d => d.ParentComment)
                    .WithMany(p => p.InverseParentComment)
                    .HasForeignKey(d => d.ParentCommentId)
                    .HasConstraintName("FK_Comment_ParentComment");
            });

            modelBuilder.Entity<CommentRate>(entity =>
            {
                entity.ToTable("CommentRate");
                
                entity
                    .Property(e => e.CommentRateId)
                    .HasDefaultValueSql("NEWID()");

                entity.HasIndex(e => e.CommentId, "IX_CommentRate_CommentId");
                
                entity
                    .HasOne(d => d.Comment)
                    .WithMany(p => p.CommentRates)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_CommentRate_CommentId");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity
                    .Property(e => e.PostId)
                    .HasDefaultValueSql("NEWID()");

                entity
                    .Property(e => e.Content)
                    .IsRequired();

                entity
                    .Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(72);
            });

            modelBuilder.Entity<PostRate>(entity =>
            {
                entity.ToTable("PostRate");

                entity
                    .Property(e => e.PostRateId)
                    .HasDefaultValueSql("NEWID()");
                
                entity.HasIndex(e => e.PostId, "IX_PostRate_PostId");
                
                entity
                    .HasOne(d => d.Post)
                    .WithMany(p => p.PostRates)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostRate_PostId");
            });
        }

        #endregion
    }
}