namespace CourseManager.Domain.Entities;

public sealed class CourseInstanceTeacher
{
    public int CourseInstanceId { get; private set; }
    public int TeacherId { get; private set; }

    private CourseInstanceTeacher() { }

    public CourseInstanceTeacher(int courseInstanceId, int teacherId)
    {
        CourseInstanceId = courseInstanceId;
        TeacherId = teacherId;
    }
}