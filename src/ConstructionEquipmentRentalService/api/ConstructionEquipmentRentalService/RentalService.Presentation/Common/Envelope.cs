namespace RentalService.Common;

public sealed record Envelope<T>(T? Data, string? ErrorMessage)
{
    public static Envelope<T> Ok(T data) => new(data, null);
    public static Envelope<T> Error(string errorMessage) => new(default, errorMessage);
}
