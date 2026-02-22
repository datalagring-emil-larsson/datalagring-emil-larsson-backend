using CourseManager.Domain.Enums;
using CourseManager.Domain.Exceptions;
using System;

public sealed class Enrollment
{
    public int Id { get; private set; }
    public int ParticipantId { get; private set; }
    public int CourseInstanceId { get; private set; }
    public EnrollmentStatus Status { get; private set; }
    public DateTime RegisteredAtUtc { get; private set; }
    public DateTime ModifiedAtUtc { get; private set; }

    private Enrollment() { }

    internal Enrollment(int participantId, int courseInstanceId)
    {
        CourseInstanceId = courseInstanceId;
        ParticipantId = participantId;
        RegisteredAtUtc = DateTime.UtcNow;
        Status = EnrollmentStatus.Registered;

    }

    public void Cancel()
    {
        Status = EnrollmentStatus.Cancelled;
        ModifiedAtUtc = DateTime.UtcNow;
    } 
    public void MarkAttended()
    {
        Status = EnrollmentStatus.Attended;
        ModifiedAtUtc = DateTime.UtcNow;
    } 

    public void Reactivate()
    {
        Status = EnrollmentStatus.Registered;
        ModifiedAtUtc = DateTime.UtcNow;
    }
    
}
