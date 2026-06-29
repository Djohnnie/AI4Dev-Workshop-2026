using ShortUrlWorkshop.Domain;

namespace ShortUrlWorkshop.Repositories;

public interface IShortLinkRepository
{
    Task<bool> TryAddAsync(ShortLink shortLink, CancellationToken cancellationToken);

    Task<ShortLink?> GetByCodeAsync(string code, CancellationToken cancellationToken);

    Task<ShortLink?> RegisterRedirectAsync(string code, DateTimeOffset redirectedAtUtc, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ShortLink>> ListAsync(CancellationToken cancellationToken);
}
