namespace CourseManager.Application.Features.Participants.UpdateParticipant;

public sealed record UpdateParticipantRequest(string FirstName, string LastName, string Email, string? PhoneNumber = null);

