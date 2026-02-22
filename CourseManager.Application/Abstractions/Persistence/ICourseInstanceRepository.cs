using CourseManager.Domain.Entities;

namespace CourseManager.Application.Abstractions.Persistence;

public interface ICourseInstanceRepository
{
    Task<CourseInstance?> GetWithEnrollmentsAsync(int id, CancellationToken ct);
    Task SaveChangesAsync(CancellationToken ct);
    Task<CourseInstance?> GetWithEnrollmentsByEnrollmentIdAsync(int enrollmentId, CancellationToken ct);
    Task<CourseInstance?> GetWithCourseAndLocationAsync(int Id, CancellationToken ct);
    Task<List<CourseInstance>> ListWithCourseAndLocationAsync(CancellationToken ct);
}
