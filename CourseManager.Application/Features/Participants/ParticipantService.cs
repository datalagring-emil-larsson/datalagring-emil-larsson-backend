using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Common;
using CourseManager.Application.Features.Participants.DTOs;
using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.Participants;

public sealed class ParticipantService
{
    private readonly IBaseRepository<Participant> _repo;

    public ParticipantService(IBaseRepository<Participant> repo) => _repo = repo;

    public async Task<int> CreateAsync(CreateParticipantRequest request, CancellationToken ct)
    {
        var participant = new Participant(request.FirstName, request.LastName, request.Email, request.PhoneNumber);

        await _repo.AddAsync(participant, ct);
        await _repo.SaveChangesAsync(ct);

        return participant.Id;
    }

    public async Task<ParticipantResult> GetByIdAsync(int id, CancellationToken ct)
    {
        var participant = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Participant", id);

        return new ParticipantResult(participant.Id, participant.FirstName, participant.LastName, participant.Email, participant.PhoneNumber);
    }

    public async Task<List<ParticipantResult>> ListAsync(CancellationToken ct)
    {
        var participants = await _repo.ListAsync(ct);

        return participants.Select(p => new ParticipantResult(p.Id, p.FirstName, p.LastName, p.Email, p.PhoneNumber)).ToList();
    }

    public async Task UpdateAsync(int id, UpdateParticipantRequest request, CancellationToken ct)
    {
        var participant = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Participant", id);
        
        participant.Update(request.FirstName, request.LastName, request.Email, request.PhoneNumber);
        
        await _repo.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct)
    {
        var participant = await _repo.GetByIdAsync(id, ct)
            ?? throw new NotFoundException("Participant", id);
        
        _repo.Remove(participant);

        await _repo.SaveChangesAsync(ct);
    }
}
