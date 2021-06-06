using System;
using System.Linq;
using System.Reflection;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Primary.DataAccess
{
    internal sealed class XNewsDbContext : DbContext, IXNewsDbContext, IXNewsDbContextExtended
    {
        #region Constructors

        public XNewsDbContext(DbContextOptions<XNewsDbContext> options) : base(options)
        {
        }

        #endregion

        #region IXNewsDbContext

        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentRate> CommentRate { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostRate> PostRate { get; set; }
        
        #endregion

        #region IXNewsDbContextExtended

        public IQueryable<Comment> GetPostComments(Guid postId, int pageNumber, int pageSize)
        {
            return Comment.FromSqlInterpolated
            (@$"
                SELECT CommentId, UserId, Content, PostId, ParentCommentId 
                FROM dbo.fn_PostCommentHierarchy({postId}, {pageNumber}, {pageSize})
            ");
        }

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