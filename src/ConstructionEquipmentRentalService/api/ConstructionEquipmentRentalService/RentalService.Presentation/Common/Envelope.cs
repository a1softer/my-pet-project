using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace RentalService.Common;

public sealed record Envelope<T>(T? Data, string? ErrorMessage, int StatusCode) : IResult
{
    public static Envelope<T> Ok(T data) => new(data, null, StatusCodes.Status200OK);
    public static Envelope<T> BadRequest(string errorMessage) => new(default, errorMessage, StatusCodes.Status400BadRequest);
    public static Envelope<T> NotFound(string errorMessage) => new(default, errorMessage, StatusCodes.Status404NotFound);
    public static Envelope<T> Conflict(string errorMessage) => new(default, errorMessage, StatusCodes.Status409Conflict);
    public static Envelope<T> InternalError(string errorMessage) => new(default, errorMessage, StatusCodes.Status500InternalServerError);

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var response = httpContext.Response;

        response.ContentType = "application/json; charset=utf-8";

        response.StatusCode = StatusCode;

        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await response.WriteAsync(json);
    }
}
