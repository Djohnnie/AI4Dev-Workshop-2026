namespace ShortUrlWorkshop.Models;

public sealed record ShortLinkDetailsResponse(
    string Code,
    string ShortUrl,
    string DestinationUrl,
    DateTimeOffset CreatedAtUtc,
    int RedirectCount,
    DateTimeOffset? LastRedirectedAtUtc);
