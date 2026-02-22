namespace CourseManager.Application.Features.Participants.DTOs;

public sealed record UpdateParticipantRequest(string FirstName, string LastName, string Email, string? PhoneNumber = null);

