using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class TeacherConfig : IEntityTypeConfiguration<Teacher>
{
    public void Configure( EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teacher");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedOnAdd();

        builder.Property(t => t.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.LastName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(t => t.Email)
            .HasMaxLength(255)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(t => t.Expertise)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(t => t.Email)
            .IsUnique();
    }
}
