using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public sealed class Location
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;

    private Location() { }

    public Location(string name, string address, string city)
    {
        Id = Guid.NewGuid();
        Name = name.Trim();
        Address = address.Trim();
        City = city.Trim();
        if (string.IsNullOrWhiteSpace(Name))
            throw new DomainException("Locationname cannot be empty.");
    }
}