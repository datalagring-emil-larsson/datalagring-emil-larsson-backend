using CourseManager.Application.Features.Teachers;
using CourseManager.Application.Features.Teachers.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class TeacherEndpoints
{
    public static IEndpointRouteBuilder MapTeacherEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/teachers", async (CreateTeacherRequest request, TeacherService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);
            return Results.Created($"/teachers/{id}", new { id });
        });

        app.MapGet("/teachers", async (TeacherService service, CancellationToken ct) =>
        {
            var teachers = await service.ListAsync(ct);
            return Results.Ok(teachers);
        });

        app.MapGet("/teachers/{id:guid}", async (Guid id, TeacherService service, CancellationToken ct) =>
        {
            var teacher = await service.GetByIdAsync(id, ct);
            return Results.Ok(teacher);
        });

        app.MapPut("/teachers/{id:guid}", async (Guid id, UpdateTeacherRequest request, TeacherService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        app.MapDelete("/teachers/{id:guid}", async (Guid id, TeacherService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
