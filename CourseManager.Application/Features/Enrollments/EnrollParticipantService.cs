using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;

namespace CourseManager.Application.Features.Enrollments;

public sealed class EnrollParticipantService
{
    private readonly ICourseInstanceRepository _repo;

    public EnrollParticipantService(ICourseInstanceRepository repo)
    {
        _repo = repo;
    }

    public async Task<EnrollParticipantResult> HandleAsync(EnrollParticipantRequest request, CancellationToken ct)
    {
        var courseInstance = await _repo.GetWithEnrollmentsAsync(request.CourseInstanceId, ct)
            ?? throw new NotFoundException("CourseInstance", request.CourseInstanceId);

        var enrollment = courseInstance.Enroll(request.ParticipantId, DateTime.UtcNow);

        await _repo.SaveChangesAsync(ct);

        return new EnrollParticipantResult(enrollment.Id);
    }
}
