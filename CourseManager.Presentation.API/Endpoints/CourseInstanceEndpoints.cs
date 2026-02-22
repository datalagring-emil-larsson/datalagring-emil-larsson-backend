using CourseManager.Application.Features.CourseInstances;
using CourseManager.Application.Features.CourseInstances.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class CourseInstancesEndpoints
{
    public static IEndpointRouteBuilder MapCourseInstanceEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/course-instances", async (CreateCourseInstanceRequest request, CourseInstanceService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/course-instances/{id}", new { id });
        });

        app.MapGet("/course-instances", async (CourseInstanceService service, CancellationToken ct) =>
        {
            var items = await service.ListAsync(ct);
            return Results.Ok(items);
        });

        app.MapGet("/course-instances/{id:guid}", async (Guid id, CourseInstanceService service, CancellationToken ct) =>
        {
            var ci = await service.GetByIdAsync(id, ct);
            return Results.Ok(ci);
        });

        app.MapPut("/course-instances/{id:guid}", async (Guid id, UpdateCourseInstanceRequest request, CourseInstanceService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        app.MapDelete("/course-instances/{id:guid}", async (Guid id, CourseInstanceService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
