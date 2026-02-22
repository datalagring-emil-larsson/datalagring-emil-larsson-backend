using CourseManager.Application.Features.Courses;
using CourseManager.Application.Features.Courses.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class CourseEndpoints
{
    public static IEndpointRouteBuilder MapCoursesEndpoints(this IEndpointRouteBuilder app)
    {

        var group = app.MapGroup("/courses")
            .WithTags("Courses");


        group.MapPost("/", async(CreateCourseRequest request, CourseService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/{id}", new { id });
        });

        group.MapGet("/list", async (CourseService service, CancellationToken ct) =>
        {
            var courses = await service.ListAsync(ct);
            return Results.Ok(courses);
        });

        group.MapGet("/{id:int}", async (int id, CourseService service, CancellationToken ct) =>
        {
            var course = await service.GetByIdAsync(id, ct);
            return Results.Ok(course);
        });

        group.MapPut("/{id:int}", async (int id, UpdateCourseRequest request, CourseService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        group.MapDelete("{id:int}", async (int id, CourseService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
