using CourseManager.Domain.Exceptions;

namespace CourseManager.Domain.Entities;

public class CourseInstance
{
    public int Id { get; private set; }
    public int CourseId { get; private set; }
    public Course CourseDetail { get; private set; } = null!;
    public int LocationId { get; private set; }
    public Location LocationDetail { get; private set; } = null!;
    public DateTime StartDateUtc { get; private set; }
    public DateTime EndDateUtc { get; private set; }
    public int Capacity { get; private set; }

    public ICollection<Enrollment> Enrollments { get; private set; } = new List<Enrollment>();

    public ICollection<CourseInstanceTeacher> Teachers { get; private set; } = new List<CourseInstanceTeacher>();

    private CourseInstance() { }

    public CourseInstance(int courseId, int locationId, DateTime startDate, DateTime endDate, int capacity)
    {
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

    public void AssignTeacher(int teacherId)
    {
        if (Teachers.Any(t => t.TeacherId == teacherId))
            throw new InvalidOperationException("Teacher is already assigned to this course instance.");

        Teachers.Add(new CourseInstanceTeacher(Id, teacherId));

    }

    public void UnassignTeacher(int teacherId)
    {
        var teacher = Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
        if (teacher is null)
            throw new DomainException("Teacher is not assigned to this course instance.");

        Teachers.Remove(teacher);
    }

    public Enrollment Enroll(int participantId)
    {
        if (Enrollments.Count(e => e.Status == Enums.EnrollmentStatus.Registered) >= Capacity)
            throw new DomainException("Course instance is at full capacity.");

        var existing = Enrollments.SingleOrDefault(e => e.ParticipantId == participantId);

        if (existing is not null)
        {
            if (existing.Status == Enums.EnrollmentStatus.Registered)
                throw new DomainException("Participant is already enrolled.");
        }

        var enrollment = new Enrollment(participantId, Id);
        Enrollments.Add(enrollment);
        return enrollment;
    }

    public void Update(int courseId, int locationId, DateTime startDateUtc, DateTime endDateUtc, int capacity)
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