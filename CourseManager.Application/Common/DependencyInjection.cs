using CourseManager.Application.Features.CourseInstances;
using CourseManager.Application.Features.CourseInstanceTeacher;
using CourseManager.Application.Features.Courses;
using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using CourseManager.Application.Features.Locations;
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

        services.AddScoped<LocationService>();

        services.AddScoped<CourseInstanceService>();

        services.AddScoped<CourseInstanceTeacherService>();

        return services;
    }
}
