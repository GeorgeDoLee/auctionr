namespace AuctionR.Core.API.ExceptionHandling;

public static class ProblemTypeUrls
{
    public const string BadRequest = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    public const string NotFound = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
    public const string InternalServerError = "https://tools.ietf.org/html/rfc7231#section-6.6.1";
    public const string Unauthorized = "https://tools.ietf.org/html/rfc7235#section-3.1";
    public const string Forbidden = "https://tools.ietf.org/html/rfc7231#section-6.5.3";
}
