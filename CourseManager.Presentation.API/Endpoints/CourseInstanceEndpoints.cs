using CourseManager.Application.Features.CourseInstances;
using CourseManager.Application.Features.CourseInstances.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class CourseInstancesEndpoints
{
    public static IEndpointRouteBuilder MapCourseInstanceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/course-Instances")
            .WithTags("Course Instances");

        group.MapPost("/", async (CreateCourseInstanceRequest request, CourseInstanceService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/course-instances/{id}", new { id });
        });

        group.MapGet("/", async (CourseInstanceService service, CancellationToken ct) =>
        {
            var items = await service.ListAsync(ct);
            return Results.Ok(items);
        });

        group.MapGet("/{id:guid}", async (Guid id, CourseInstanceService service, CancellationToken ct) =>
        {
            var ci = await service.GetByIdAsync(id, ct);
            return Results.Ok(ci);
        });

        group.MapPut("/{id:guid}", async (Guid id, UpdateCourseInstanceRequest request, CourseInstanceService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:guid}", async (Guid id, CourseInstanceService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
