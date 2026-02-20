using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.Teachers.DTOs;
using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.Teachers;

public sealed class TeacherService
{
    private readonly IBaseRepository<Teacher> _repo;

    public TeacherService(IBaseRepository<Teacher> repo) => _repo = repo;

    public async Task<Guid> CreateAsync(CreateTeacherRequest request, CancellationToken ct)
    {
        var teacher = new Teacher(Guid.NewGuid(), request.FirstName, request.LastName, request.Email, request.Expertise);

        await _repo.AddAsync(teacher, ct);
        await _repo.SaveChangesAsync(ct);

        return teacher.Id;
    }

    public async Task<Teacher> GetByIdAsync(Guid id, CancellationToken ct) 
        => await _repo.GetByIdAsync(id, ct)
        ?? throw new NotFoundException("Teacher", id);

    public Task<List<Teacher>> ListAsync(CancellationToken ct)
        => _repo.ListAsync(ct);

    public async Task UpdateAsync(Guid id, UpdateTeacherRequest request, CancellationToken ct)
    {
        var teacher = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Teacher", id);

        teacher.Update(request.FirstName, request.LastName, request.Email, request.Expertise);
        await _repo.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var teacher = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Teacher", id);

        _repo.Remove(teacher);
        await _repo.SaveChangesAsync(ct);
    }
}
