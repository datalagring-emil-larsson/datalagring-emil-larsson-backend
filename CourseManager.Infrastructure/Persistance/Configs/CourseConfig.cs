using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class CourseConfig : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever();

        builder.Property(c => c.CourseCode)
            .HasMaxLength(20)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(c => c.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasMaxLength(500)
            .IsRequired();

        builder.HasIndex(c => c.CourseCode)
            .IsUnique();
    }
}
