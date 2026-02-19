using CourseManager.Application.Abstractions.Persistence;
using CourseManager.Infrastructure.Persistance;
using CourseManager.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var conectionString = config.GetConnectionString("CourseManagerDbConnectionString");

        services.AddDbContext<CourseManagerDbContext>(options =>
            options.UseSqlServer(conectionString, sql => sql.MigrationsAssembly(typeof(CourseManagerDbContext).Assembly.FullName)));

        services.AddScoped<ICourseInstanceRepository, CourseInstanceRepository>();

        return services;
    }
}
