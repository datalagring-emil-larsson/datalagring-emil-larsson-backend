using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Infrastructure.Persistance.Repositories;

public sealed class CourseInstanceRepository : BaseRepository<CourseInstance>, ICourseInstanceRepository
{

    public CourseInstanceRepository(CourseManagerDbContext context) : base(context)
    {
    }        

    public Task<CourseInstance?> GetWithEnrollmentsAsync(Guid id, CancellationToken ct) =>
        _context.CourseInstances
        .Include(ci => ci.Teachers)
        .Include(ci => ci.Enrollments)
        .SingleOrDefaultAsync(ci => ci.Id == id, ct);
}
