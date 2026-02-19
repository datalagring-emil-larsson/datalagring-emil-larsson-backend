using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.Participants.CreateParticipant;
using CourseManager.Application.Features.Participants.UpdateParticipant;
using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.Participants;

public sealed class ParticipantService
{
    private readonly IBaseRepository<Participant> _repo;

    public ParticipantService(IBaseRepository<Participant> repo) => _repo = repo;

    public async Task<Guid> CreateAsync(CreateParticipantRequest request, CancellationToken ct)
    {
        var participant = new Participant(request.FirstName, request.LastName, request.Email, request?.PhoneNumber);
        await _repo.AddAsync(participant, ct);
        await _repo.SaveChangesAsync(ct);
        return participant.Id;
    }

    public async Task<Participant> GetByIdAsync(Guid id, CancellationToken ct)
        => await _repo.GetByIdAsync(id, ct)
        ?? throw new NotFoundException("Participant", id);

    public Task<List<Participant>> ListAsync(CancellationToken ct)
        => _repo.ListAsync(ct);

    public async Task UpdateAsync(Guid id, UpdateParticipantRequest request, CancellationToken ct)
    {
        var participant = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Participant", id);
        
        participant.Update(request.FirstName, request.LastName, request.Email, request?.PhoneNumber);
        
        await _repo.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var participant = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Participant", id);
        
        _repo.Remove(participant);

        await _repo.SaveChangesAsync(ct);
    }
}
