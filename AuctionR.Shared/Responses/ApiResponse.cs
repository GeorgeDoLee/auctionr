namespace AuctionR.Shared.Responses;

public record ApiResponse<T>(bool Success, string? Message = null, T? Data = default)
{
    public static ApiResponse<T> SuccessResponse(string message, T? data = default) =>
        new(true, message, data);

    public static ApiResponse<T> FailResponse(string message, T? data = default) =>
        new(false, message, data);
}