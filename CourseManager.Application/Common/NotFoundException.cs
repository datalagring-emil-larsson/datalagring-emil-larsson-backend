namespace CourseManager.Application.Common;

public sealed class NotFoundException : Exception
{
    public NotFoundException(string entity, object key)
        : base($"Entity \"{entity}\" with key \"{key}\" was not found.")
    {
    }
}
