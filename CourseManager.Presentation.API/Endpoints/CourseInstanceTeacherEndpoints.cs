using CourseManager.Application.Features.CourseInstanceTeacher;

namespace CourseManager.Presentation.API.Endpoints;

public static class CourseInstanceTeacherEndpoints
{
    public static IEndpointRouteBuilder MapCourseInstanceTeachersEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/course-instance-teacher")
            .WithTags("Assign/Unassign teacher");

        group.MapPost("/AssignTeacher", async (
            int courseInstanceId, 
            int teacherId, 
            CourseInstanceTeacherService service, 
            CancellationToken ct) =>
        {
            await service.AssignTeacher(courseInstanceId, teacherId, ct);
            return Results.NoContent();
        });

        group.MapDelete("/UnassignTeacher", async (
            int courseInstanceId,
            int teacherId, 
            CourseInstanceTeacherService service, 
            CancellationToken ct) =>
        {
            await service.UnassignTeacher(courseInstanceId, teacherId, ct);
            return Results.NoContent();
        });

        group.MapGet("/Teachers-CourseInstance-List", async (int courseInstanceId, CourseInstanceTeacherService service, CancellationToken ct) =>
        {
            var teacherIds = await service.ListTeacherIdAsync(courseInstanceId, ct);
            return Results.Ok(teacherIds);
        });

        return app;
    }
}
