using CourseManager.Application.Features.Participants;
using CourseManager.Application.Features.Participants.CreateParticipant;
using CourseManager.Application.Features.Participants.UpdateParticipant;

namespace CourseManager.Presentation.API.Endpoints;

public static class ParticipantsEndpoint
{
    public static IEndpointRouteBuilder MapParticipantsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/participants")
            .WithTags("Participants");

        group.MapPost("/", async (
            CreateParticipantRequest request,
            ParticipantService service,
            CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/participants/{id}", new { id });
        });

        group.MapGet("/list", async (
            ParticipantService service,
            CancellationToken ct) =>
        {
            var participants = await service.ListAsync(ct);
            return Results.Ok(participants);
        });

        group.MapGet("/{id:guid}", async (
            Guid id,
            ParticipantService service, CancellationToken ct) =>
        {
            var participant = await service.GetByIdAsync(id, ct);
            return Results.Ok(participant);
        });

        group.MapPut("/{id:guid}", async (
            Guid id,
            UpdateParticipantRequest request,
            ParticipantService service,
            CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (
            Guid id,
            ParticipantService service,
            CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
