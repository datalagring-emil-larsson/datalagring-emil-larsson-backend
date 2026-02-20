using CourseManager.Application.Features.Locations;
using CourseManager.Application.Features.Locations.DTOs;

namespace CourseManager.Presentation.API.Endpoints;

public static class LocationEndpoints
{
    public static IEndpointRouteBuilder MapLocationEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("/locations", async (CreateLocationRequest request, LocationService service, CancellationToken ct) =>
        {
            var id = await service.CreateAsync(request, ct);

            return Results.Created($"/locations/{id}", new { id });
        });

        app.MapGet("/location", async (LocationService service, CancellationToken ct) =>
        {
            var Locations = await service.ListAsync(ct);
            return Results.Ok(Locations);
        });

        app.MapGet("/locations/{id:guid}", async (Guid id, LocationService service, CancellationToken ct) =>
        {
            var location = await service.GetByIdAsync(id, ct);
            return Results.Ok(location);
        });

        app.MapPut("/locations/{id:guid}", async (Guid id, UpdateLocationRequest request, LocationService service, CancellationToken ct) =>
        {
            await service.UpdateAsync(id, request, ct);
            return Results.NoContent();
        });

        app.MapDelete("/locations/{id:guid}", async (Guid id, LocationService service, CancellationToken ct) =>
        {
            await service.DeleteAsync(id, ct);
            return Results.NoContent();
        });

        return app;
    }
}
