using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.Courses.DTOs;
using CourseManager.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace CourseManager.Application.Features.Courses;

public sealed class CourseService
{
    private readonly IBaseRepository<Course> _repo;

    public CourseService(IBaseRepository<Course> repo) => _repo = repo;

    public async Task<int> CreateAsync(CreateCourseRequest request, CancellationToken ct)
    {
        var course = new Course(request.CourseCode, request.Title, request.Description);

        await _repo.AddAsync(course, ct);
        await _repo.SaveChangesAsync(ct);

        return course.Id;
    }

    public async Task<CourseResult> GetByIdAsync(int id, CancellationToken ct)
        {
        var course = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Course", id);

        return new CourseResult(course.Id, course.CourseCode, course.Title, course.Description);
    }

    public async Task<List<CourseResult>> ListAsync(CancellationToken ct) 
    { 
        var courses = await _repo.ListAsync(ct); 
    
        return courses.Select(c => new CourseResult(c.Id, c.CourseCode, c.Title, c.Description)).ToList();
    }

    

    public async Task UpdateAsync(int id, UpdateCourseRequest request, CancellationToken ct)
    {
        var course = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Course", id);

        course.Update(request.CourseCode, request.Title, request.Description);
        await _repo.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var course = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Course", id);

        _repo.Remove(course);
        await _repo.SaveChangesAsync(ct);
    }
}
