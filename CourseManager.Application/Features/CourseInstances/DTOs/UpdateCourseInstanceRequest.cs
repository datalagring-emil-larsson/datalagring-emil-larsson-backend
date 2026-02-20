namespace CourseManager.Application.Features.CourseInstances.DTOs;

public sealed record UpdateCourseInstanceRequest(Guid CourseId, Guid LocationId, DateTime StartDateUtc, DateTime EndDateUtc, int Capacity);
