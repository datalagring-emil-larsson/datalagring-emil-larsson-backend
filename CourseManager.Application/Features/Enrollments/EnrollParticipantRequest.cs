namespace CourseManager.Application.Features.Enrollments;

public sealed record EnrollParticipantRequest(Guid CourseInstanceId, Guid ParticipantId);

