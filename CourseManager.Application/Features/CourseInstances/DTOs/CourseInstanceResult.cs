namespace CourseManager.Application.Features.CourseInstances.DTOs;

public sealed record CourseInstanceResult(int Id, int CourseId, CourseSummaryResult CourseDetail, int LocationId, LocationSummaryResult LocationDetail, DateTime StartDateUtc, DateTime EndDateUtc, int Capacity);
