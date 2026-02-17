namespace CourseManager.Domain.Entities;

public sealed class CourseInstanceTeacher
{
    public Guid CourseInstanceId { get; private set; }
    public Guid TeacherId { get; private set; }

    private CourseInstanceTeacher() { }

    public CourseInstanceTeacher(Guid courseInstanceId, Guid teacherId)
    {
        CourseInstanceId = courseInstanceId;
        TeacherId = teacherId;
    }
}