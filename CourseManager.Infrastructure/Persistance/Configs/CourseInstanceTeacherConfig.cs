using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class CourseInstanceTeacherConfig : IEntityTypeConfiguration<CourseInstanceTeacher>
{
    public void Configure(EntityTypeBuilder<CourseInstanceTeacher> builder)
    {
        builder.ToTable("CourseInstanceTeacher");

        builder.HasKey(cit => new {cit.CourseInstanceId, cit.TeacherId});

        builder.Property(cit => cit.CourseInstanceId)
            .IsRequired();

        builder.Property(cit => cit.TeacherId)
            .IsRequired();

        builder.HasIndex(cit => new { cit.CourseInstanceId, cit.TeacherId })
            .IsUnique();
    }

}
