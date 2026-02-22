using CourseManager.Domain.Enums;
using CourseManager.Domain.Exceptions;
using System;

public sealed class Enrollment
{
    public Guid Id { get; private set; }
    public Guid ParticipantId { get; private set; }
    public Guid CourseInstanceId { get; private set; }
    public EnrollmentStatus Status { get; private set; }
    public DateTime RegisteredAtUtc { get; private set; }

    private Enrollment() { }

    internal Enrollment(Guid id, Guid participantId, Guid courseInstanceId, DateTime registeredAtUtc)
    {
        Id = id;
        CourseInstanceId = courseInstanceId;
        ParticipantId = participantId;
        RegisteredAtUtc = registeredAtUtc;
        Status = EnrollmentStatus.Registered;
    }

    public void Cancel() => Status = EnrollmentStatus.Cancelled;
    public void MarkAttended() => Status = EnrollmentStatus.Attended;

    
}
