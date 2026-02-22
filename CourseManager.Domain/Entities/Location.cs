using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public sealed class Location
{
    public int Id { get; private set; }
    public string Classroom { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string City { get; private set; } = null!;

    private Location() { }

    public Location(string classRoom, string address, string city)
    {
        Update(classRoom, address, city);

    }

    public void Update(string classRoom, string address, string city)
    {
        Classroom = classRoom.Trim();
        Address = address.Trim();
        City = city.Trim();


        if (string.IsNullOrWhiteSpace(classRoom))
            throw new DomainException("StreetName cannot be empty.");

        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException("Adress cannot be empty.");

        if (string.IsNullOrWhiteSpace(city))
            throw new DomainException("City cannot be empty.");
    }
}