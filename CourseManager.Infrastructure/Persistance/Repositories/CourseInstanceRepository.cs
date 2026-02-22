using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Infrastructure.Persistance.Repositories;

public sealed class CourseInstanceRepository : BaseRepository<CourseInstance>, ICourseInstanceRepository
{

    public CourseInstanceRepository(CourseManagerDbContext context) : base(context)
    {
    }        

    public Task<CourseInstance?> GetWithEnrollmentsAsync(int id, CancellationToken ct)
        =>
        _context.CourseInstances
        .Include(ci => ci.Teachers)
        .Include(ci => ci.Enrollments)
        .SingleOrDefaultAsync(ci => ci.Id == id, ct);

    public async Task<CourseInstance?> GetWithEnrollmentsByEnrollmentIdAsync(int enrollmentId, CancellationToken ct)
    {
        var courseInstanceId = await _context.Enrollments
            .Where(e => e.Id == enrollmentId)
            .Select(e => e.CourseInstanceId)
            .SingleOrDefaultAsync();

        if (courseInstanceId == 0) return null; // Changed from Guid.Empty to 0 for int type

        return await _context.CourseInstances
            .Include(ci => ci.Enrollments)
            .SingleOrDefaultAsync(ci => ci.Id == courseInstanceId, ct);
    }
        

}
