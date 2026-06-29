using System.Collections.Concurrent;
using ShortUrlWorkshop.Domain;

namespace ShortUrlWorkshop.Repositories;

public sealed class InMemoryShortLinkRepository : IShortLinkRepository
{
    private readonly ConcurrentDictionary<string, ShortLink> _links = new(StringComparer.OrdinalIgnoreCase);

    public Task<bool> TryAddAsync(ShortLink shortLink, CancellationToken cancellationToken)
    {
        return Task.FromResult(_links.TryAdd(shortLink.Code, shortLink));
    }

    public Task<ShortLink?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        _links.TryGetValue(code, out var shortLink);
        return Task.FromResult(shortLink);
    }

    public Task<ShortLink?> RegisterRedirectAsync(string code, DateTimeOffset redirectedAtUtc, CancellationToken cancellationToken)
    {
        if (!_links.TryGetValue(code, out var shortLink))
        {
            return Task.FromResult<ShortLink?>(null);
        }

        lock (shortLink)
        {
            shortLink.RegisterRedirect(redirectedAtUtc);
        }

        return Task.FromResult<ShortLink?>(shortLink);
    }

    public Task<IReadOnlyCollection<ShortLink>> ListAsync(CancellationToken cancellationToken)
    {
        IReadOnlyCollection<ShortLink> links = _links.Values
            .OrderByDescending(link => link.CreatedAtUtc)
            .ToArray();

        return Task.FromResult(links);
    }
}
