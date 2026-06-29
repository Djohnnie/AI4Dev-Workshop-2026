using System.Text.RegularExpressions;
using ShortUrlWorkshop.Domain;
using ShortUrlWorkshop.Models;
using ShortUrlWorkshop.Repositories;

namespace ShortUrlWorkshop.Services;

public sealed class ShortLinkService : IShortLinkService
{
    private static readonly Regex CustomCodePattern = new("^[A-Za-z0-9_-]{4,24}$", RegexOptions.Compiled);

    private readonly IShortCodeGenerator _shortCodeGenerator;
    private readonly IShortLinkRepository _shortLinkRepository;

    public ShortLinkService(IShortLinkRepository shortLinkRepository, IShortCodeGenerator shortCodeGenerator)
    {
        _shortLinkRepository = shortLinkRepository;
        _shortCodeGenerator = shortCodeGenerator;
    }

    public async Task<CreateShortLinkResult> CreateAsync(CreateShortLinkRequest request, CancellationToken cancellationToken)
    {
        var errors = ValidateRequest(request, out var destinationUrl, out var customCode);
        if (errors.Count > 0)
        {
            return new CreateShortLinkResult(CreateShortLinkStatus.ValidationFailed, Errors: errors);
        }

        if (customCode is not null)
        {
            var requestedLink = new ShortLink(customCode, destinationUrl!, DateTimeOffset.UtcNow);
            var added = await _shortLinkRepository.TryAddAsync(requestedLink, cancellationToken);
            return added
                ? new CreateShortLinkResult(CreateShortLinkStatus.Success, requestedLink)
                : new CreateShortLinkResult(CreateShortLinkStatus.Conflict, Message: "That short code is already in use.");
        }

        for (var attempt = 0; attempt < 20; attempt++)
        {
            var generatedCode = _shortCodeGenerator.Generate();
            var generatedLink = new ShortLink(generatedCode, destinationUrl!, DateTimeOffset.UtcNow);

            if (await _shortLinkRepository.TryAddAsync(generatedLink, cancellationToken))
            {
                return new CreateShortLinkResult(CreateShortLinkStatus.Success, generatedLink);
            }
        }

        return new CreateShortLinkResult(
            CreateShortLinkStatus.Conflict,
            Message: "Unable to generate a unique short code after multiple attempts.");
    }

    public Task<ResolveShortLinkResult> ResolveAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Task.FromResult(new ResolveShortLinkResult(ResolveShortLinkStatus.NotFound));
        }

        return ResolveKnownCodeAsync(code.Trim(), cancellationToken);
    }

    public Task<ShortLink?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Task.FromResult<ShortLink?>(null);
        }

        return _shortLinkRepository.GetByCodeAsync(code.Trim(), cancellationToken);
    }

    public Task<IReadOnlyCollection<ShortLink>> ListAsync(CancellationToken cancellationToken)
    {
        return _shortLinkRepository.ListAsync(cancellationToken);
    }

    private async Task<ResolveShortLinkResult> ResolveKnownCodeAsync(string code, CancellationToken cancellationToken)
    {
        var shortLink = await _shortLinkRepository.RegisterRedirectAsync(code, DateTimeOffset.UtcNow, cancellationToken);
        return shortLink is null
            ? new ResolveShortLinkResult(ResolveShortLinkStatus.NotFound)
            : new ResolveShortLinkResult(ResolveShortLinkStatus.Success, shortLink.DestinationUrl);
    }

    private static Dictionary<string, string[]> ValidateRequest(
        CreateShortLinkRequest request,
        out string? destinationUrl,
        out string? customCode)
    {
        var errors = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        destinationUrl = null;
        customCode = null;

        if (string.IsNullOrWhiteSpace(request.Url))
        {
            errors["url"] = ["Destination URL is required."];
        }
        else if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var destinationUri) ||
                 (destinationUri.Scheme != Uri.UriSchemeHttp && destinationUri.Scheme != Uri.UriSchemeHttps))
        {
            errors["url"] = ["Destination URL must be an absolute http or https URL."];
        }
        else
        {
            destinationUrl = destinationUri.AbsoluteUri;
        }

        if (!string.IsNullOrWhiteSpace(request.CustomCode))
        {
            customCode = request.CustomCode.Trim();

            if (!CustomCodePattern.IsMatch(customCode))
            {
                errors["customCode"] =
                [
                    "Custom codes must be 4-24 characters and use only letters, numbers, hyphens, or underscores."
                ];
            }
        }

        return errors;
    }
}
