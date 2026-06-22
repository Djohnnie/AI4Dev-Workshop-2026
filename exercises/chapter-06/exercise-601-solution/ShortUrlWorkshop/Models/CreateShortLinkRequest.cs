namespace ShortUrlWorkshop.Models;

public sealed record CreateShortLinkRequest(string Url, string? CustomCode);
