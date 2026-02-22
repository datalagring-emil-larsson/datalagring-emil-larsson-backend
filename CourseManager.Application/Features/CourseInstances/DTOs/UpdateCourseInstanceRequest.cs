namespace CourseManager.Application.Features.CourseInstances.DTOs;

public sealed record UpdateCourseInstanceRequest(int CourseId, int LocationId, DateTime StartDateUtc, DateTime EndDateUtc, int Capacity);
