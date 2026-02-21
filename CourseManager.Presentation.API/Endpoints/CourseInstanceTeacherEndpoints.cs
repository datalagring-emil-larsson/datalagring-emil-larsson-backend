using CourseManager.Application.Features.CourseInstanceTeacher;

namespace CourseManager.Presentation.API.Endpoints;

public static class CourseInstanceTeacherEndpoints
{
    public static IEndpointRouteBuilder MapCourseInstanceTeachersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/course-instances/{courseInstanceId:guid}/teachers/AssignTeacher", async (
            Guid courseInstanceId, 
            Guid teacherId, 
            CourseInstanceTeacherService service, 
            CancellationToken ct) =>
        {
            await service.AssignTeacher(courseInstanceId, teacherId, ct);
            return Results.NoContent();
        });

        app.MapDelete("/course-instances/{courseInstanceId:guid}/teachers/UnassignTeacher", async (
            Guid courseInstanceId,
            Guid teacherId, 
            CourseInstanceTeacherService service, 
            CancellationToken ct) =>
        {
            await service.UnassignTeacher(courseInstanceId, teacherId, ct);
            return Results.NoContent();
        });

        app.MapGet("/course-instances/{courseInstanceId:guid}/teachers", async (Guid courseInstanceId, CourseInstanceTeacherService service, CancellationToken ct) =>
        {
            var teacherIds = await service.ListTeacherIdAsync(courseInstanceId, ct);
            return Results.Ok(teacherIds);
        });

        return app;
    }
}
