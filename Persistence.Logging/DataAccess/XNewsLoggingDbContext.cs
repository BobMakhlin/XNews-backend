using Domain.Logging.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Logging.DataAccess
{
    public sealed class XNewsLoggingDbContext : DbContext
    {
        #region Constructors

        public XNewsLoggingDbContext(DbContextOptions<XNewsLoggingDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        #endregion

        #region Properties

        public DbSet<ApplicationLog> Logs { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationLog>(entity =>
            {
                entity
                    .Property(log => log.ApplicationLogId)
                    .ValueGeneratedOnAdd();

                entity
                    .Property(log => log.MachineName)
                    .HasMaxLength(50)
                    .IsRequired();

                entity
                    .Property(log => log.LoggedAt)
                    .IsRequired();

                entity
                    .Property(log => log.Level)
                    .HasMaxLength(50)
                    .IsRequired();
                
                entity
                    .Property(log => log.Message)
                    .IsRequired();

                entity
                    .Property(log => log.Logger)
                    .HasMaxLength(250);
            });
        }

        #endregion
    }
}