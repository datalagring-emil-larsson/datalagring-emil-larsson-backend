using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class ParticipantConfig : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder) 
    {
        builder.ToTable("Participants");
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Email)
            .HasMaxLength(255)
            .IsUnicode(false)
            .IsRequired();

        builder.Property(p => p.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.LastName)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.PhoneNumber)
            .HasMaxLength(15)
            .IsRequired(false)
            .IsUnicode(false);
    }
}
