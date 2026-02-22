using CourseManager.Domain.Enums;

namespace CourseManager.Application.Features.Participants.DTOs;

public sealed record ParticipantResult(int Id, string FirstName, string LastName, string Email, string? PhoneNumber = null);
