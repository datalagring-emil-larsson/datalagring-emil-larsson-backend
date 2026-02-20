using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.CourseInstances.DTOs;
using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.CourseInstances;

public sealed class CourseInstanceService
{
    private readonly IBaseRepository<CourseInstance> _courseInstance;
    private readonly IBaseRepository<Course> _course;
    private readonly IBaseRepository<Location> _location;

    public CourseInstanceService(IBaseRepository<CourseInstance> courseInstance, IBaseRepository<Course> course, IBaseRepository<Location> location)
    {
        _courseInstance = courseInstance;
        _course = course;
        _location = location;
    }

    public async Task<Guid> CreateAsync(CreateCourseInstanceRequest request, CancellationToken ct)
    {
        if (await _course.GetByIdAsync(request.CourseId, ct) is null)
            throw new NotFoundException("Course", request.CourseId);

        if (await _location.GetByIdAsync(request.LocationId, ct) is null)
            throw new NotFoundException("Location", request.LocationId);

        var ci = new CourseInstance(request.CourseId, request.LocationId, request.StartDateUtc, request.EndDateUtc, request.Capacity);

        await _courseInstance.AddAsync(ci, ct);
        await _courseInstance.SaveChangesAsync(ct);

        return ci.Id;
    }

    public async Task<CourseInstance> GetByIdAsync(Guid id, CancellationToken ct)
        => await _courseInstance.GetByIdAsync(id, ct)
        ?? throw new NotFoundException("CourseInstance", id);

    public Task<List<CourseInstance>> ListAsync(CancellationToken ct)
        => _courseInstance.ListAsync(ct);

    public async Task UpdateAsync(Guid id, UpdateCourseInstanceRequest request, CancellationToken ct)
    {
        var ci = await _courseInstance.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("CourseInstance", id);

        if (await _course.GetByIdAsync(request.CourseId, ct) is null)
            throw new NotFoundException("Course", request.CourseId);

        if (await _location.GetByIdAsync(request.LocationId, ct) is null)
            throw new NotFoundException("Location", request.LocationId);

        ci.Update(request.CourseId, request.LocationId, request.StartDateUtc, request.EndDateUtc, request.Capacity);

        await _courseInstance.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var ci = await _courseInstance.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("CourseInstace", id);

        _courseInstance.Remove(ci);
        await _courseInstance.SaveChangesAsync(ct);
    }
}
