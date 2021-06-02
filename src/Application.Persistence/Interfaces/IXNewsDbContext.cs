using System.Threading;
using System.Threading.Tasks;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence.Interfaces
{
    public interface IXNewsDbContext
    {
        public DbSet<Category> Category { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentRate> CommentRate { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<PostRate> PostRate { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}