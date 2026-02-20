namespace CourseManager.Application.Features.Courses.DTOs;

public sealed record CreateCourseRequest(string CourseCode, string Title, string Description);

