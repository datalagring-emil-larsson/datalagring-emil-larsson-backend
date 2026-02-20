using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public sealed class Teacher
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Expertise { get; private set; } = null!;

    private Teacher() { }

    public Teacher(Guid id, string firstName, string lastName, string email, string expertise)
    {
        Id = id;
        Update(firstName, lastName, email, expertise);
    }

    public void Update(string firstName, string lastName, string email, string expertise)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        Expertise = expertise.Trim();

        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("Firstname is required");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Lastname is required.");

        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("Email is required.");

        if (string.IsNullOrWhiteSpace(expertise))
            throw new DomainException("Expertise is required");

    }
}
