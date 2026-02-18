using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class CourseInstanceConfig : IEntityTypeConfiguration<CourseInstance>
{
    public void Configure(EntityTypeBuilder<CourseInstance> builder)
    {
        builder.ToTable("CourseInstances");
        builder.HasKey(ci => ci.Id);

        builder.Property(ci => ci.Id)
            .ValueGeneratedNever();

        builder.Property(ci => ci.Capacity)
            .IsRequired();

        builder.Property(ci => ci.StartDateUtc)
            .IsRequired();

        builder.Property(ci => ci.EndDateUtc)
            .IsRequired();

        builder.HasMany(ci => ci.Enrollments)
            .WithOne()
            .HasForeignKey(e => e.CourseInstanceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(ci => ci.Teachers)
            .WithOne()
            .HasForeignKey(k => k.CourseInstanceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
