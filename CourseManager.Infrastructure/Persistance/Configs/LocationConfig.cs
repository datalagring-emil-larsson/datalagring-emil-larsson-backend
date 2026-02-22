using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseManager.Infrastructure.Persistance.Configs;

public sealed class LocationConfig : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.ToTable("Locations");
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        builder.Property(l => l.Classroom)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.Address)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(l => l.City)
            .HasMaxLength(100)
            .IsRequired();
            
    }
}
