using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class EnrollmentConfig : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.ParticipantId)
            .IsRequired();

        builder.Property(e => e.CourseInstanceId)
            .IsRequired();

        builder.Property(e => e.RegisteredAtUtc)
            .IsRequired();

        builder.Property(e => e.ModifiedAtUtc)
            .IsRequired();

        builder.Property(e => e.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.HasIndex(e => new { e.ParticipantId, e.CourseInstanceId }).IsUnique();
    }
}
