using CourseManager.Application.Common;
using CourseManager.Domain.Exceptions;
using CourseManager.Infrastructure;
using CourseManager.Infrastructure.Persistance;
using CourseManager.Presentation.API.Endpoints;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var ex = feature?.Error;

        context.Response.ContentType = "application/problem+json";

        if (ex is NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            return;
        }

        if (ex is DomainException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            return;
        }
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { error = "Unexpected error." });
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapEnrollmentsEndpoints();
app.MapDummyDataEndpoints();
app.MapParticipantsEndpoints();
app.MapCoursesEndpoints();

app.MapGet("/health", () => Results.Ok("OK"));



app.Run();