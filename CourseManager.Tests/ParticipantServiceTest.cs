using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Application.Features.Participants;
using CourseManager.Application.Features.Participants.DTOs;
using CourseManager.Domain.Entities;
using NSubstitute;

namespace CourseManager.Tests;

public class ParticipantServiceTest
{
    [Fact]
    public async Task CreateAsync_ShouldAddParticipant_SaveChanges_AndReturnId()
    {
        var repo = Substitute.For<IBaseRepository<Participant>>();
        var participantService = new ParticipantService(repo);

        var request = new CreateParticipantRequest(
            FirstName: "Emil",
            LastName: "Larsson",
            Email: "emil@domain.se",
            PhoneNumber: "1234567890"
            );

        var ct = CancellationToken.None;

        repo.AddAsync(Arg.Any<Participant>(), ct)
            .Returns(Task.CompletedTask);

        repo.SaveChangesAsync(ct)
            .Returns(Task.CompletedTask);


        await participantService.CreateAsync(request, ct);

        await repo.Received(1).AddAsync(
            Arg.Is<Participant>(p =>
            p.FirstName == "Emil" &&
            p.LastName == "Larsson" &&
            p.Email == "emil@domain.se" &&
            p.PhoneNumber == "1234567890"
            ), ct);

        await repo.Received(1).SaveChangesAsync(ct);
    }
}
