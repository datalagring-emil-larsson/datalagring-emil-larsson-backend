using CourseManager.Domain.Entities;

namespace CourseManager.Application.Features.Enrollments.EnrollParticipant;

public sealed record EnrollParticipantRequest(Guid courseInstanceId, Guid ParticipantId);

