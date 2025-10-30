using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace RentalService.Common;

public sealed record Envelope<T>(T? Data, string? ErrorMessage) : IResult
{
    public static Envelope<T> Ok(T data) => new(data, null);
    public static Envelope<T> Error(string errorMessage) => new(default, errorMessage);

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        var response = httpContext.Response;

        response.ContentType = "application/json; charset=utf-8";

        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            response.StatusCode = ErrorMessage switch
            {
                string msg when msg.Contains("не найдено") || msg.Contains("не найден") => StatusCodes.Status404NotFound,
                string msg when msg.Contains("уже завершено") || msg.Contains("уже забронировано") => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status400BadRequest
            };
        }
        else
        {
            response.StatusCode = StatusCodes.Status200OK;
        }

        var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await response.WriteAsync(json);
    }
}
