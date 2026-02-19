using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public sealed class Participant
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }

    private Participant() { }

    public Participant(string firstName, string lastName, string email, string? phoneNumber = null)
    {
        Id = Guid.NewGuid();
        Update(firstName, lastName, email, phoneNumber);        
    }

    public void Update(string firstName, string lastName, string email, string? phoneNumber = null)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        PhoneNumber = phoneNumber?.Trim();

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new DomainException("First name is required.");

        if (string.IsNullOrWhiteSpace(LastName))
            throw new DomainException("Last name is required.");

        if (string.IsNullOrWhiteSpace(Email))
            throw new DomainException("Email is required.");
    }
}