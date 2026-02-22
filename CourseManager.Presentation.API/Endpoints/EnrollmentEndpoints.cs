using CourseManager.Application.Features.Enrollments;
using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using CourseManager.Domain.Entities;

namespace CourseManager.Presentation.API.Endpoints;

public static class EnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapEnrollmentsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/enrollment")
            .WithTags("Enrollment");


        group.MapPost("/", async (EnrollParticipantRequest request, EnrollmentService service, CancellationToken ct) =>
        {
            var enrollmentId = await service.EnrollAsync(request, DateTime.UtcNow, ct);
            return Results.Created($"/enrollments/{enrollmentId}", new { enrollmentId });
        });

        group.MapGet("/list", async (int courseInstanceId, EnrollmentService service, CancellationToken ct) =>
        {
            var enrollments = await service.ListByCourseInstanceAsync(courseInstanceId, ct);
            return Results.Ok(enrollments);
        });

        group.MapPut("/{id:guid}/cancel", async (int id, EnrollmentService service, CancellationToken ct) =>
        {
            await service.MarkAttendedAsync(id, ct);
            return Results.NoContent();
        });

        group.MapPut("/{id:guid}/attended", async (int id, EnrollmentService service, CancellationToken ct) =>
        {
            await service.MarkAttendedAsync(id, ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (int id, EnrollmentService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });
        
        return app;
     

    }

    public sealed record EnrollRequest(Guid ParticipantId);

}

