using System.Reflection;
using Domain.Logging.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Logging.DataAccess
{
    internal sealed class XNewsLoggingDbContext : DbContext
    {
        #region Constructors

        public XNewsLoggingDbContext(DbContextOptions<XNewsLoggingDbContext> options) : base(options)
        {
        }

        #endregion

        #region Properties

        public DbSet<ApplicationLog> Logs { get; set; }

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