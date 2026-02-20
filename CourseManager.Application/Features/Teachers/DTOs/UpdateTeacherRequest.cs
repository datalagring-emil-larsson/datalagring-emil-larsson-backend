namespace CourseManager.Application.Features.Teachers.DTOs;

public sealed record UpdateTeacherRequest(string FirstName, string LastName, string Email, string Expertise);
