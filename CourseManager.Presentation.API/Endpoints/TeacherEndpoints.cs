using CourseManager.Application.Features.Teachers;
using CourseManager.Application.Features.Teachers.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class TeacherEndpoints
{
    public static IEndpointRouteBuilder MapTeacherEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/teachers")
            .WithTags("Teachers");

        group.MapPost("/", async (CreateTeacherRequest request, TeacherService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/teachers/{id}", new { id });
        });

        group.MapGet("/list", async (TeacherService service, CancellationToken ct) =>
        {
            var teachers = await service.ListAsync(ct);
            return Results.Ok(teachers);
        });

        group.MapGet("/{id:int}", async (int id, TeacherService service, CancellationToken ct) =>
        {
            var teacher = await service.GetByIdAsync(id, ct);
            return Results.Ok(teacher);
        });

        group.MapPut("/{id:int}", async (int id, UpdateTeacherRequest request, TeacherService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (int id, TeacherService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
