namespace CourseManager.Application.Features.CourseInstances.DTOs;

public sealed record CreateCourseInstanceRequest(int CourseId, int LocationId, DateTime StartDateUtc, DateTime EndDateUtc, int Capacity);

