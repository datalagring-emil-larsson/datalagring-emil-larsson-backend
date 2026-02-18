using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Infrastructure.Persistance.Repositories;

public sealed class CourseInstanceRepository : ICourseInstanceRepository
{
    private readonly CourseManagerDbContext _context;

    public CourseInstanceRepository(CourseManagerDbContext context)
    {
        _context = context; 
    }        

    public Task<CourseInstance?> GetWithEnrollmentsAsync(Guid id, CancellationToken ct) =>
        _context.CourseInstances
        .Include(ci => ci.Enrollments)
        .SingleOrDefaultAsync(ci => ci.Id == id, ct);

    public Task SaveChangesAsync(CancellationToken ct) =>
        _context.SaveChangesAsync(ct);

    //public Task<bool> ExistsAsync(Guid id, CancellationToken ct) => _context.CourseInstances.AnyAsync(ci => ci.Id == id, ct);
}
