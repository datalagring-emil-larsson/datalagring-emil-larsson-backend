using CourseManager.Domain.Enums;

public sealed class Enrollment
{
    public Guid Id { get; private set; }
    public Guid ParticipantId { get; private set; }
    public EnrollmentStatus Status { get; private set; }
    public DateTime RegisteredAtUtc { get; private set; }

    private Enrollment() { }

    internal Enrollment(Guid id, Guid participantId, DateTime registeredAtUtc)
    {
        Id = id;
        ParticipantId = participantId;
        RegisteredAtUtc = registeredAtUtc;
        Status = EnrollmentStatus.Registered;
    }

    public void Cancel() => Status = EnrollmentStatus.Cancelled;
    public void MarkAttended() => Status = EnrollmentStatus.Attended;
}
