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
    private readonly ICourseInstanceRepository _courseInstanceRepository;

    public CourseInstanceService(IBaseRepository<CourseInstance> courseInstance, IBaseRepository<Course> course, IBaseRepository<Location> location, ICourseInstanceRepository courseInstanceRepository)
    {
        _courseInstance = courseInstance;
        _course = course;
        _location = location;
        _courseInstanceRepository = courseInstanceRepository;
    }

    public async Task<int> CreateAsync(CreateCourseInstanceRequest request, CancellationToken ct)
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

    public async Task<CourseInstanceResult> GetByIdAsync(int id, CancellationToken ct)
    {
        var ci = await _courseInstanceRepository.GetWithCourseAndLocationAsync(id, ct)
            ?? throw new NotFoundException("CourseInstance", id);

        return new CourseInstanceResult(
            ci.Id, 
            ci.CourseId, new CourseSummaryResult(ci.CourseDetail.Id, ci.CourseDetail.CourseCode, ci.CourseDetail.Title), 
            ci.LocationId, new LocationSummaryResult(ci.LocationDetail.Id, ci.LocationDetail.Classroom, ci.LocationDetail.Address, ci.LocationDetail.City), 
            ci.StartDateUtc, ci.EndDateUtc, ci.Capacity
        );
    }

    public async Task<List<CourseInstanceResult>> ListAsync(CancellationToken ct)
    {
        var instances = await _courseInstanceRepository.ListWithCourseAndLocationAsync(ct);

        return instances.Select(ci => new CourseInstanceResult(
            ci.Id,
            ci.CourseId, new CourseSummaryResult(ci.CourseDetail.Id, ci.CourseDetail.CourseCode, ci.CourseDetail.Title),
            ci.LocationId, new LocationSummaryResult(ci.LocationDetail.Id, ci.LocationDetail.Classroom, ci.LocationDetail.Address, ci.LocationDetail.City),
            ci.StartDateUtc, ci.EndDateUtc, ci.Capacity
            )).ToList();

        
    }

    public async Task UpdateAsync(int id, UpdateCourseInstanceRequest request, CancellationToken ct)
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

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var ci = await _courseInstance.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("CourseInstace", id);

        _courseInstance.Remove(ci);
        await _courseInstance.SaveChangesAsync(ct);
    }
}
