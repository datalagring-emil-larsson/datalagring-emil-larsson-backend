using CourseManager.Application.Features.Courses;
using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using CourseManager.Application.Features.Teachers;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManager.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EnrollParticipantService>();

        services.AddScoped<CourseService>();

        services.AddScoped<TeacherService>();

        return services;
    }
}
