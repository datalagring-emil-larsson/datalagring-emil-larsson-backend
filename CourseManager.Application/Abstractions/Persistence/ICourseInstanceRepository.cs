using CourseManager.Domain.Entities;

namespace CourseManager.Application.Abstractions.Persistence;

public interface ICourseInstanceRepository
{
    Task<CourseInstance?> GetWithEnrollmentsAsync(int id, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    Task<CourseInstance?> GetWithEnrollmentsByEnrollmentIdAsync(int enrollmentId, CancellationToken ct);
}
