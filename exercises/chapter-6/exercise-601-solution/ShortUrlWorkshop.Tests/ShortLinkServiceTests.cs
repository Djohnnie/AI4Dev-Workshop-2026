using ShortUrlWorkshop.Models;
using ShortUrlWorkshop.Repositories;
using ShortUrlWorkshop.Services;

namespace ShortUrlWorkshop.Tests;

public sealed class ShortLinkServiceTests
{
    [Fact]
    public async Task CreateAsync_WithInvalidUrl_ReturnsValidationFailure()
    {
        var service = CreateService();

        var result = await service.CreateAsync(new CreateShortLinkRequest("not-a-url", null), CancellationToken.None);

        Assert.Equal(CreateShortLinkStatus.ValidationFailed, result.Status);
        Assert.NotNull(result.Errors);
        Assert.Contains("url", result.Errors!.Keys);
    }

    [Fact]
    public async Task CreateAsync_WithCustomCode_ReturnsRequestedAlias()
    {
        var service = CreateService();

        var result = await service.CreateAsync(
            new CreateShortLinkRequest("https://github.com/features/copilot", "copilot"),
            CancellationToken.None);

        Assert.Equal(CreateShortLinkStatus.Success, result.Status);
        Assert.Equal("copilot", result.Link!.Code);
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateCustomCode_ReturnsConflict()
    {
        var service = CreateService();

        await service.CreateAsync(
            new CreateShortLinkRequest("https://learn.microsoft.com/dotnet/", "dotnet"),
            CancellationToken.None);

        var duplicateResult = await service.CreateAsync(
            new CreateShortLinkRequest("https://github.com/dotnet/", "dotnet"),
            CancellationToken.None);

        Assert.Equal(CreateShortLinkStatus.Conflict, duplicateResult.Status);
    }

    [Fact]
    public async Task ResolveAsync_IncrementsRedirectStatistics()
    {
        var service = CreateService();

        var createResult = await service.CreateAsync(
            new CreateShortLinkRequest("https://github.com/features/copilot", "statsdemo"),
            CancellationToken.None);

        var resolveResult = await service.ResolveAsync("statsdemo", CancellationToken.None);
        var storedLink = await service.GetByCodeAsync("statsdemo", CancellationToken.None);

        Assert.Equal(ResolveShortLinkStatus.Success, resolveResult.Status);
        Assert.NotNull(storedLink);
        Assert.Equal(1, storedLink!.RedirectCount);
        Assert.NotNull(storedLink.LastRedirectedAtUtc);
        Assert.Equal(createResult.Link!.DestinationUrl, resolveResult.DestinationUrl);
    }

    [Fact]
    public async Task CreateAsync_WhenGeneratedCodeCollides_RetriesUntilUnique()
    {
        var service = CreateService(new StubShortCodeGenerator("taken01", "unique02"));

        await service.CreateAsync(
            new CreateShortLinkRequest("https://github.com/", "taken01"),
            CancellationToken.None);

        var result = await service.CreateAsync(
            new CreateShortLinkRequest("https://learn.microsoft.com/", null),
            CancellationToken.None);

        Assert.Equal(CreateShortLinkStatus.Success, result.Status);
        Assert.Equal("unique02", result.Link!.Code);
    }

    private static ShortLinkService CreateService(IShortCodeGenerator? shortCodeGenerator = null)
    {
        return new ShortLinkService(
            new InMemoryShortLinkRepository(),
            shortCodeGenerator ?? new StubShortCodeGenerator("alpha01"));
    }

    private sealed class StubShortCodeGenerator : IShortCodeGenerator
    {
        private readonly Queue<string> _codes;

        public StubShortCodeGenerator(params string[] codes)
        {
            _codes = new Queue<string>(codes);
        }

        public string Generate(int length = 7)
        {
            return _codes.Count > 0 ? _codes.Dequeue() : "fallback1";
        }
    }
}
