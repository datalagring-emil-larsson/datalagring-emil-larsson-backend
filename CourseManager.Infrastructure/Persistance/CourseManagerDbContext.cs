using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Infrastructure.Persistance;

public sealed class CourseManagerDbContext : DbContext
{
    public CourseManagerDbContext(DbContextOptions<CourseManagerDbContext> options) : base(options) { }
    
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseInstance> CourseInstances => Set<CourseInstance>();
    public DbSet<CourseInstanceTeacher> CourseInstanceTeachers => Set<CourseInstanceTeacher>();
    public DbSet<Enrollment> Enrollments => Set<Enrollment>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Participant> Participants => Set<Participant>();
    public DbSet<Teacher> Teachers => Set<Teacher>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CourseManagerDbContext).Assembly);
    }
}
