using System.ComponentModel.DataAnnotations;

namespace CourseManager.Application.Features.Participants.DTOs;

public sealed record CreateParticipantRequest(
    [Required] string FirstName, 
    [Required]string LastName, 
    [Required, EmailAddress]string Email, 
    string? PhoneNumber = null
);

