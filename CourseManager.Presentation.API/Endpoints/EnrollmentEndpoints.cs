using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using CourseManager.Domain.Entities;

namespace CourseManager.Presentation.API.Endpoints;

public static class EnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapEnrollmentsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/course-instance/{courseInstanceId:guid}/enrollments", async (
            Guid courseInstanceId,
            EnrollRequest body,
            EnrollParticipantService service,
            CancellationToken ct) =>
        {

            var result = await service.HandleAsync(
                new EnrollParticipantRequest(courseInstanceId, body.ParticipantId), ct);

            return Results.Created($"/enrollments/{result.EnrollmentId}", result);
        });
        
        return app;
     

    }

    public sealed record EnrollRequest(Guid ParticipantId);

}

