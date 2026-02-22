//using CourseManager.Application.Abstractions.Persistence;
//using CourseManager.Application.Common;

//namespace CourseManager.Application.Features.Enrollments.EnrollParticipant;

//public sealed class EnrollParticipantService(ICourseInstanceRepository repo)
//{
//    private readonly ICourseInstanceRepository _repo = repo;

//    public async Task<EnrollParticipantResult> HandleAsync(EnrollParticipantRequest request, CancellationToken ct)
//    {
//        var courseInstance = await _repo.GetWithEnrollmentsAsync(request.courseInstanceId, ct)
//            ?? throw new NotFoundException("CourseInstance", request.courseInstanceId);

//        var enrollment = courseInstance.Enroll(request.ParticipantId, DateTime.UtcNow);

//        await _repo.SaveChangesAsync(ct);

//        return new EnrollParticipantResult(enrollment.Id);
//    }
//}
