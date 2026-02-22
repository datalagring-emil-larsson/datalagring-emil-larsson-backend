using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.Locations.DTOs;
using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.Locations;

public sealed class LocationService
{
    private readonly IBaseRepository<Location> _repo;

    public LocationService(IBaseRepository<Location> repo) => _repo = repo;

    public async Task<int> CreateAsync(CreateLocationRequest request, CancellationToken ct)
    {
        var location = new Location(request.classRoom, request.adress, request.city);

        await _repo.AddAsync(location, ct);
        await _repo.SaveChangesAsync(ct);

        return location.Id;
    }

    public async Task<Location> GetByIdAsync(int id, CancellationToken ct)
        => await _repo.GetByIdAsync(id, ct)
        ?? throw new NotFoundException("Location", id);

    public Task<List<Location>> ListAsync(CancellationToken ct)
        => _repo.ListAsync(ct);

    public async Task UpdateAsync(int id, UpdateLocationRequest request, CancellationToken ct)
    {
        var location = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Location", id);

        location.Update(request.classRoom, request.adress, request.city);
        await _repo.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var location = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Location", id);

        _repo.Remove(location);
        await _repo.SaveChangesAsync(ct);
    }
}
