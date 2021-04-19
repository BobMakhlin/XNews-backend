using System.Reflection;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Primary.DataAccess
{
    internal sealed class XNewsDbContext : DbContext, IXNewsDbContext
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

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        #endregion
    }
}