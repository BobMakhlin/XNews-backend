using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Persistence.Interfaces;
using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Primary.Seeders
{
    /// <summary>
    /// A seeder type used to populate <see cref="IXNewsDbContext"/> with data.
    /// </summary>
    public class XNewsDbSeeder : IDbSeeder
    {
        #region Fields

        private readonly IXNewsDbContext _newsDbContext;
        private readonly ILogger<XNewsDbSeeder> _logger;

        #endregion

        #region Constructors

        public XNewsDbSeeder(IXNewsDbContext newsDbContext, ILogger<XNewsDbSeeder> logger)
        {
            _newsDbContext = newsDbContext;
            _logger = logger;
        }

        #endregion

        #region IDbSeeder

        public async Task SeedAsync(CancellationToken cancellationToken = default)
        {
            bool isNeedToSeed = await IsNeedToSeedAsync(cancellationToken)
                .ConfigureAwait(false);
            if (!isNeedToSeed)
            {
                _logger.LogInformation("There is no need to seed");
                return;
            }

            IEnumerable<Category> categories = GetCategories(20);
            IEnumerable<Post> posts = GetPosts(100);

            await _newsDbContext.Category.AddRangeAsync(categories, cancellationToken)
                .ConfigureAwait(false);
            await _newsDbContext.Post.AddRangeAsync(posts, cancellationToken)
                .ConfigureAwait(false);

            await _newsDbContext.SaveChangesAsync(cancellationToken)
                .ConfigureAwait(false);
            
            _logger.LogInformation("The database populated with data");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks is need to seed the database with data.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> IsNeedToSeedAsync(CancellationToken cancellationToken = default)
        {
            bool postsDbSetEmpty = !await _newsDbContext.Post.AnyAsync(cancellationToken)
                .ConfigureAwait(false);
            bool categoriesDbSetEmpty = !await _newsDbContext.Category.AnyAsync(cancellationToken)
                .ConfigureAwait(false);
            return postsDbSetEmpty && categoriesDbSetEmpty;
        }

        /// <summary>
        /// Creates the specified <paramref name="count"/> of categories.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private IEnumerable<Category> GetCategories(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new() {Title = $"Category {i}"};
            }
        }

        /// <summary>
        /// Creates the specified <paramref name="count"/> of posts.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private IEnumerable<Post> GetPosts(int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return new()
                {
                    Title = $"Post {i}",
                    Content = $"Post {i} content..."
                };
            }
        }

        #endregion
    }
}