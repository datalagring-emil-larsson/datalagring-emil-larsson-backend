using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;

namespace CourseManager.Application.Features.Enrollments.EnrollParticipant;

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

        //Console.WriteLine($"Loaded CourseInstance: {courseInstance.Id}"); //tillfällig
        //Console.WriteLine($"Enrollments loaded: {courseInstance.Enrollments.Count}"); //tillf'llig

        var enrollment = courseInstance.Enroll(request.ParticipantId, DateTime.UtcNow);

        //var exists = await _repo.ExistsAsync(request.CourseInstanceId, ct); //tillfällig
        //Console.WriteLine($"Exists in DB: {exists}"); //tillfällig

        await _repo.SaveChangesAsync(ct);

        return new EnrollParticipantResult(enrollment.Id);
    }
}
