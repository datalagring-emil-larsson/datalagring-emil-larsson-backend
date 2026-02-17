using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public class CourseInstance
{
    public Guid Id { get; private set; }
    public Guid CourseId { get; private set; }
    public Guid LocationId { get; private set; }
    public DateTime StartDateUtc { get; private set; }
    public DateTime EndDateUtc { get; private set; }
    public int Capacity { get; private set; }
    private readonly List<Enrollment> _enrollments = new();
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments;


    private readonly List<CourseInstanceTeacher> _teachers = new();
    public IReadOnlyCollection<CourseInstanceTeacher> Teachers => _teachers;

    private CourseInstance() { }

    public CourseInstance(Guid courseId, Guid locationId, DateTime startDate, DateTime endDate, int capacity)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        LocationId = locationId;
        StartDateUtc = startDate;
        EndDateUtc = endDate;
        Capacity = capacity;

        if (StartDateUtc <= EndDateUtc)
            throw new ArgumentException("Start date must be before end date.");

        if (Capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    public void AssignTeacher(Guid teacherId)
    {
        if (_teachers.Any(t => t.TeacherId == teacherId))
            throw new InvalidOperationException("Teacher is already assigned to this course instance.");
        _teachers.Add(new CourseInstanceTeacher(Id, teacherId));
    }

    public Enrollment Enroll(Guid participantId, DateTime nowUtc)
    {
        if (_enrollments.Count(e => e.Status == Enums.EnrollmentStatus.Registered) >= Capacity)
            throw new DomainException("Course instance is at full capacity.");

        if (_enrollments.Any(e => e.ParticipantId == participantId && e.Status == Enums.EnrollmentStatus.Registered))
            throw new DomainException("Participant is already enrolled.");

        var enrollment = new Enrollment(Guid.NewGuid(), participantId, Id, nowUtc);
        _enrollments.Add(enrollment);
        return enrollment;
    }

}