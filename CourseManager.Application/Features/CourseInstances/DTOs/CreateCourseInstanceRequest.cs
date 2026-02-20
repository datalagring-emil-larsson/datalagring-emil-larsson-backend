namespace CourseManager.Application.Features.CourseInstances.DTOs;

public sealed record CreateCourseInstanceRequest(Guid CourseId, Guid LocationId, DateTime StartDateUtc, DateTime EndDateUtc, int Capacity);

