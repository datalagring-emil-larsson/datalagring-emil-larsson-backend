using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.CourseInstanceTeacher;

public sealed class CourseInstanceTeacherService
{
    private readonly ICourseInstanceRepository _courseInstances;
    private readonly IBaseRepository<Teacher> _teachers;

    public CourseInstanceTeacherService(ICourseInstanceRepository courseInstances, IBaseRepository<Teacher> teachers)
    {
        _courseInstances = courseInstances;
        _teachers = teachers;
    }

    public async Task AssignTeacher(int courseInstanceId, int TeacherId, CancellationToken ct)
    {
        var ci = await _courseInstances.GetWithEnrollmentsAsync(courseInstanceId, ct)
            ?? throw new NotFoundException("CourseInstance", courseInstanceId);

        if (await _teachers.GetByIdAsync(TeacherId, ct)is null)
            throw new NotFoundException("Teacher", TeacherId);

        ci.AssignTeacher(TeacherId);

        await _courseInstances.SaveChangesAsync(ct);
    }

    public async Task UnassignTeacher(int courseInstanceId, int TeacherId, CancellationToken ct)
    {
        var ci = await _courseInstances.GetWithEnrollmentsAsync(courseInstanceId, ct)
            ?? throw new NotFoundException("CourseInstance", courseInstanceId);

        ci.UnassignTeacher(TeacherId);

        await _courseInstances.SaveChangesAsync(ct);
    }

    public async Task<List<int>> ListTeacherIdAsync(int courseInstanceId, CancellationToken ct)
    {
        var ci = await _courseInstances.GetWithEnrollmentsAsync(courseInstanceId, ct)
            ?? throw new NotFoundException("CourseInstance", courseInstanceId);

        return ci.Teachers.Select(t => t.TeacherId).ToList();
    }
}
