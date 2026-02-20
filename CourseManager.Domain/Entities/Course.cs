using CourseManager.Domain.Exceptions;
using System.Data;

namespace CourseManager.Domain.Entities;

public sealed class Course
{
    public Guid Id { get; set; }
    public string CourseCode { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;


    private Course() { }

    public Course(string courseCode, string title, string description)
    {
        Id = Guid.NewGuid();
        Update(courseCode, title, description);

    }

    public void Update(string courseCode, string title, string description)
    {
        CourseCode = courseCode.Trim();
        Title = title.Trim();
        Description = description.Trim();

        if (string.IsNullOrWhiteSpace(CourseCode))
            throw new DomainException("Course code is required");

        if (string.IsNullOrWhiteSpace(Title))
            throw new DomainException("Title is required.");

        if (string.IsNullOrWhiteSpace(Description))
            throw new DomainException("Description is required.");

    }
}