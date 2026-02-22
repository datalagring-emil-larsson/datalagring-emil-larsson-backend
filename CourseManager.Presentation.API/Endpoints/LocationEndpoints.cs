using CourseManager.Application.Features.Locations;
using CourseManager.Application.Features.Locations.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class LocationEndpoints
{
    public static IEndpointRouteBuilder MapLocationEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/locations")
            .WithTags("Locations");

        group.MapPost("/", async (CreateLocationRequest request, LocationService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);

            return Results.Created($"/locations/{id}", new { id });
        });

        group.MapGet("/list", async (LocationService service, CancellationToken ct) =>
        {
            var Locations = await service.ListAsync(ct);
            return Results.Ok(Locations);
        });

        group.MapGet("/{id:int}", async (int id, LocationService service, CancellationToken ct) =>
        {
            var location = await service.GetByIdAsync(id, ct);
            return Results.Ok(location);
        });

        group.MapPut("/{id:int}", async (int id, UpdateLocationRequest request, LocationService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        group.MapDelete("/{id:int}", async (int id, LocationService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
