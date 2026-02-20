namespace CourseManager.Application.Features.Courses.DTOs;

public sealed record UpdateCourseRequest(string CourseCode, string Title, string Description);

