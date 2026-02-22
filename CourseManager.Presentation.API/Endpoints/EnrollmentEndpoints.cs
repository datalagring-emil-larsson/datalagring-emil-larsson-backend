using CourseManager.Application.Features.Enrollments;
using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using CourseManager.Domain.Entities;

namespace CourseManager.Presentation.API.Endpoints;

public static class EnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapEnrollmentsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/enrollments", async (EnrollParticipantRequest request, EnrollmentService service, CancellationToken ct) =>
        {
            var enrollmentId = await service.EnrollAsync(request, DateTime.UtcNow, ct);
            return Results.Created($"/enrollments/{enrollmentId}", new { enrollmentId });
        });

        app.MapGet("/enrollments", async (Guid courseInstanceId, EnrollmentService service, CancellationToken ct) =>
        {
            var enrollments = await service.ListByCourseInstanceAsync(courseInstanceId, ct);
            return Results.Ok(enrollments);
        });

        app.MapPut("/enrollments/{id:guid}/cancel", async (Guid id, EnrollmentService service, CancellationToken ct) =>
        {
            await service.MarkAttendedAsync(id, ct);
            return Results.NoContent();
        });

        app.MapPut("/enrollments/{id:guid}/attended", async (Guid id, EnrollmentService service, CancellationToken ct) =>
        {
            await service.MarkAttendedAsync(id, ct);
            return Results.NoContent();
        });

        app.MapDelete("/enrollments/{id:guid}", async (Guid id, EnrollmentService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });
        
        return app;
     

    }

    public sealed record EnrollRequest(Guid ParticipantId);

}

