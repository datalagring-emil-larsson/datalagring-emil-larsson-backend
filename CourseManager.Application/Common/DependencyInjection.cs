using CourseManager.Application.Features.Enrollments.EnrollParticipant;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManager.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<EnrollParticipantService>();
        return services;
    }
}
