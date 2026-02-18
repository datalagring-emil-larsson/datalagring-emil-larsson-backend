using CourseManager.Domain.Entities;

namespace CourseManager.Application.Abstractions.Persistence;

public interface ICourseInstanceRepository
{
    Task<CourseInstance?> GetWithEnrollmentsAsync(Guid id, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    //public Task<bool> ExistsAsync(Guid id, CancellationToken ct);
}
