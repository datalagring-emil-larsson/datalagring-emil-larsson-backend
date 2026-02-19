namespace CourseManager.Application.Features.Enrollments.EnrollParticipant;

public sealed record EnrollParticipantRequest(Guid CourseInstanceId, Guid ParticipantId);

