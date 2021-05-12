using Domain.Primary.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Primary.Configurations
{
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .ToTable("Category");

            builder
                .Property(e => e.CategoryId)
                .ValueGeneratedOnAdd();
                
            builder
                .HasIndex(e => e.Title, "UQ_Category")
                .IsUnique();

            builder
                .Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(32);
        }
    }
}