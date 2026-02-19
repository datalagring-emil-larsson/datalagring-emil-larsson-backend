using CourseManager.Application.Features.Participants;
using CourseManager.Application.Features.Participants.CreateParticipant;
using CourseManager.Application.Features.Participants.UpdateParticipant;

namespace CourseManager.Presentation.API.Endpoints;

public static class ParticipantsEndpoint
{
    public static IEndpointRouteBuilder MapParticipantsEndpoints(this IEndpointRouteBuilder app)
    {

        app.MapPost("/participants", async (
            CreateParticipantRequest request,
            ParticipantService service,
            CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/participants/{id}", new { id });
        });

        app.MapGet("/participants", async (
            ParticipantService service,
            CancellationToken ct) =>
        {
            var participants = await service.ListAsync(ct);
            return Results.Ok(participants);
        });

        app.MapGet("/participants/{id:guid}", async (
            Guid id,
            ParticipantService service, CancellationToken ct) =>
        {
            var participant = await service.GetByIdAsync(id, ct);
            return Results.Ok(participant);
        });

        app.MapPut("/participant/{id:guid}", async (
            Guid id,
            UpdateParticipantRequest request,
            ParticipantService service,
            CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        app.MapDelete("/participants/{id:guid}", async (
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
