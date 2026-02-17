using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public sealed class Teacher
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string expertise { get; private set; } = null!;

    private Teacher() { }

    public Teacher(Guid id, string firstName, string lastName, string email)
    {
        Id = id;
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();

        if (string.IsNullOrWhiteSpace(FirstName))
            throw new DomainException("Firstname is required.");
        if (string.IsNullOrWhiteSpace(LastName))
            throw new DomainException("Lastname is required.");
        if (string.IsNullOrWhiteSpace(Email))
            throw new DomainException("Email is required.");
    }
}
