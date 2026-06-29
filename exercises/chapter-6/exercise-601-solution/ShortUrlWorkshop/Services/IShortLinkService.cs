using ShortUrlWorkshop.Domain;
using ShortUrlWorkshop.Models;

namespace ShortUrlWorkshop.Services;

public interface IShortLinkService
{
    Task<CreateShortLinkResult> CreateAsync(CreateShortLinkRequest request, CancellationToken cancellationToken);

    Task<ResolveShortLinkResult> ResolveAsync(string code, CancellationToken cancellationToken);

    Task<ShortLink?> GetByCodeAsync(string code, CancellationToken cancellationToken);

    Task<IReadOnlyCollection<ShortLink>> ListAsync(CancellationToken cancellationToken);
}

public enum CreateShortLinkStatus
{
    Success,
    ValidationFailed,
    Conflict
}

public sealed record CreateShortLinkResult(
    CreateShortLinkStatus Status,
    ShortLink? Link = null,
    Dictionary<string, string[]>? Errors = null,
    string? Message = null);

public enum ResolveShortLinkStatus
{
    Success,
    NotFound
}

public sealed record ResolveShortLinkResult(
    ResolveShortLinkStatus Status,
    string? DestinationUrl = null);
