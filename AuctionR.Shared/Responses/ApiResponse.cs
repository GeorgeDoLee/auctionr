namespace AuctionR.Shared.Responses;

public record ApiResponse<T>(bool Success, string? Message = null, T? Data = default)
{
    public static ApiResponse<T> SuccessResponse(T data, string? message = null) =>
        new(true, message, data);

    public static ApiResponse<T> FailResponse(string message) =>
        new(false, message);
}