namespace CourseManager.Application.Features.Participants.CreateParticipant;

public sealed record CreateParticipantRequest(string FirstName, string LastName, string Email, string? PhoneNumber = null);

