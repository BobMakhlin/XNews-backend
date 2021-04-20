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

            List<Category> categories = GetCategories();
            List<Post> posts = GetPosts();

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

        private List<Category> GetCategories()
        {
            return new()
            {
                new() {Title = "Fun"},
                new() {Title = "Mars"},
                new() {Title = "Musk"}
            };
        }

        private List<Post> GetPosts()
        {
            return new()
            {
                new()
                {
                    Title = "Elon Musk went to Mars",
                    Content = "We are on Mars today!",
                    PostRates = new List<PostRate>
                    {
                        new() {Rate = 1}, 
                        new() {Rate = 1}
                    }
                },
                new()
                {
                    Title = "Play basketball with the president",
                    Content = "Something here...",
                    PostRates = new List<PostRate>
                    {
                        new() {Rate = 1}
                    }
                }
            };
        }

        #endregion
    }
}