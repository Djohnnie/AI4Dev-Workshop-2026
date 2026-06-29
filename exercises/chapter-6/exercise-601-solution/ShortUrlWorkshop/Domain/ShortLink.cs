namespace ShortUrlWorkshop.Domain;

public sealed class ShortLink
{
    public ShortLink(string code, string destinationUrl, DateTimeOffset createdAtUtc)
    {
        Code = code;
        DestinationUrl = destinationUrl;
        CreatedAtUtc = createdAtUtc;
    }

    public string Code { get; }

    public string DestinationUrl { get; }

    public DateTimeOffset CreatedAtUtc { get; }

    public int RedirectCount { get; private set; }

    public DateTimeOffset? LastRedirectedAtUtc { get; private set; }

    public void RegisterRedirect(DateTimeOffset redirectedAtUtc)
    {
        RedirectCount++;
        LastRedirectedAtUtc = redirectedAtUtc;
    }
}
