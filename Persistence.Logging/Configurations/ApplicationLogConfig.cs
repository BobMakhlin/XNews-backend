using Domain.Logging.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Logging.Configurations
{
    public class ApplicationLogConfig : IEntityTypeConfiguration<ApplicationLog>
    {
        public void Configure(EntityTypeBuilder<ApplicationLog> builder)
        {
            builder
                .Property(log => log.ApplicationLogId)
                .ValueGeneratedOnAdd();

            builder
                .Property(log => log.MachineName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(log => log.LoggedAt)
                .IsRequired();

            builder
                .Property(log => log.Level)
                .HasMaxLength(50)
                .IsRequired();
                
            builder
                .Property(log => log.Message)
                .IsRequired();

            builder
                .Property(log => log.Logger)
                .HasMaxLength(250);
        }
    }
}