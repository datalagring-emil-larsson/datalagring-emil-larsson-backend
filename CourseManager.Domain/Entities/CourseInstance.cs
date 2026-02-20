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

    public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    public ICollection<CourseInstanceTeacher> Teachers { get; private set; } = new List<CourseInstanceTeacher>();

    private CourseInstance() { }

    public CourseInstance(Guid courseId, Guid locationId, DateTime startDate, DateTime endDate, int capacity)
    {
        Id = Guid.NewGuid();
        CourseId = courseId;
        LocationId = locationId;
        StartDateUtc = startDate;
        EndDateUtc = endDate;
        Capacity = capacity;

        if (StartDateUtc >= EndDateUtc)
            throw new ArgumentException("Start date must be before end date.");

        if (Capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");
    }

    public void AssignTeacher(Guid teacherId)
    {
        if (Teachers.Any(t => t.TeacherId == teacherId))
            throw new InvalidOperationException("Teacher is already assigned to this course instance.");
        Teachers.Add(new CourseInstanceTeacher(Id, teacherId));
    }

    public void UnassignTeacher(Guid teacherId)
    {
        var teacher = Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
        if (teacher is null)
            throw new DomainException("Teacher is not assigned to this course instance.");

        Teachers.Remove(teacher);
    }

    public Enrollment Enroll(Guid participantId, DateTime nowUtc)
    {
        if (Enrollments.Count(e => e.Status == Enums.EnrollmentStatus.Registered) >= Capacity)
            throw new DomainException("Course instance is at full capacity.");

        if (Enrollments.Any(e => e.ParticipantId == participantId && e.Status == Enums.EnrollmentStatus.Registered))
            throw new DomainException("Participant is already enrolled.");

        var enrollment = new Enrollment(Guid.NewGuid(), participantId, Id, nowUtc);
        Enrollments.Add(enrollment);
        return enrollment;
    }

    public void Update(Guid courseId, Guid locationId, DateTime startDateUtc, DateTime endDateUtc, int capacity)
    {
        CourseId = courseId;
        LocationId = locationId;
        StartDateUtc = startDateUtc;
        EndDateUtc = endDateUtc;
        Capacity = capacity;

        if (StartDateUtc >= EndDateUtc)
            throw new ArgumentException("Start date must be before end date.");

        if (Capacity <= 0)
            throw new ArgumentException("Capacity must be greater than zero.");
    }

}