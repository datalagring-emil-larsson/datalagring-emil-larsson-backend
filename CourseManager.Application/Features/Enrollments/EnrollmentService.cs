using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using CourseManager.Domain.Entities;
using System.Runtime.InteropServices;

namespace CourseManager.Application.Features.Enrollments;

public sealed class EnrollmentService
{
    private readonly ICourseInstanceRepository _courseInstances;
    private readonly IBaseRepository<Participant> _participants;

    public EnrollmentService(ICourseInstanceRepository courseInstances, IBaseRepository<Participant> participants)
    {
        _courseInstances = courseInstances;
        _participants = participants;
    }

    public async Task<Guid> EnrollAsync(EnrollParticipantRequest request, DateTime nowUtc, CancellationToken ct)
    {
        var ci = await _courseInstances.GetWithEnrollmentsAsync(request.courseInstanceId, ct)
            ?? throw new NotFoundException("CourseInstance", request.courseInstanceId);

        if (await _participants.GetByIdAsync(request.ParticipantId, ct) is null)
            throw new NotFoundException("Participant", request.ParticipantId);

        var enrollment = ci.Enroll(request.ParticipantId, nowUtc);

        await _courseInstances.SaveChangesAsync(ct);
        return enrollment.Id;
    }

    public async Task<List<object>> ListByCourseInstanceAsync(Guid courseInstanceId, CancellationToken ct)
    {
        var ci = await _courseInstances.GetWithEnrollmentsAsync(courseInstanceId, ct)
            ?? throw new NotFoundException("CourseInstance", courseInstanceId);

        return ci.Enrollments.Select(e => (object)new
        {
            id = e.Id,
            participantId = e.ParticipantId,
            courseInstanceId = e.CourseInstanceId,
            status = e.Status.ToString(),
            registeredAtUtc = e.RegisteredAtUtc
        }).ToList();
    }

    public async Task CancelAsync(Guid EnrollmentId, CancellationToken ct)
    {
        var ci = await FindCourseInstanceContainingEnrollment(EnrollmentId, ct);
        var enrollment = ci.Enrollments.Single(x => x.Id == EnrollmentId);

        enrollment.Cancel();
        await _courseInstances.SaveChangesAsync(ct);
    }

    public async Task MarkAttendedAsync(Guid EnrollmentId, CancellationToken ct)
    {
        var ci = await FindCourseInstanceContainingEnrollment(EnrollmentId, ct);
        var enrollment = ci.Enrollments.Single(x => x.Id == EnrollmentId);

        enrollment.MarkAttended();
        await _courseInstances.SaveChangesAsync(ct);
    }
    public async Task DeleteAsync(Guid EnrollmentId, CancellationToken ct)
    {
        var ci = await FindCourseInstanceContainingEnrollment(EnrollmentId, ct);
        var enrollment = ci.Enrollments.Single(x => x.Id == EnrollmentId);

        ci.Enrollments.Remove(enrollment);
        await _courseInstances.SaveChangesAsync(ct);
    }

    private async Task<CourseInstance> FindCourseInstanceContainingEnrollment(Guid enrollmentId, CancellationToken ct)
    {
        var ci = await _courseInstances.GetWithEnrollmentsByEnrollmentIdAsync(enrollmentId, ct);
        return ci ?? throw new NotFoundException("Enrollment", enrollmentId);
    }
}

