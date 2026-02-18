using CourseManager.Domain.Entities;
using CourseManager.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CourseManager.Presentation.API.Endpoints;

public static class DummyDataEndpoints
{
    public static IEndpointRouteBuilder MapDummyDataEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/seed", async (CourseManagerDbContext db) =>
        {


            var course = new Course("CS101", "Title", "Description");

            var location = new Location("Name", "Adress", "City");

            db.AddRange(course, location);
            await db.SaveChangesAsync();

            var courseInstance = new CourseInstance(course.Id, location.Id, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(5), 10);
            var participant = new Participant("Kalle", "Svensson", "Kalle@domain.se");

            db.AddRange(courseInstance, participant);
            await db.SaveChangesAsync();    

            return Results.Created("/seed", new
            {
                courseInstanceId = courseInstance.Id,
                participantId = participant.Id,
            });
        });

        return app;
    }
}
