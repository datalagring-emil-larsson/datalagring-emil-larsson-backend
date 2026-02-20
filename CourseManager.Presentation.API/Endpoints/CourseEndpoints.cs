using CourseManager.Application.Features.Courses;
using CourseManager.Application.Features.Courses.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCoursesEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/courses", async(CreateCourseRequest request, CourseService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/courses/{id}", new { id });
        });

        app.MapGet("/courses", async (CourseService service, CancellationToken ct) =>
        {
            var courses = await service.ListAsync(ct);
            return Results.Ok(courses);
        });

        app.MapGet("/courses/{id:guid}", async (Guid id, CourseService service, CancellationToken ct) =>
        {
            var course = await service.GetByIdAsync(id, ct);
            return Results.Ok(course);
        });

        app.MapPut("/courses/{id:guid}", async (Guid id, UpdateCourseRequest request, CourseService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        app.MapDelete("/courses/{id:guid}", async (Guid id, CourseService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
