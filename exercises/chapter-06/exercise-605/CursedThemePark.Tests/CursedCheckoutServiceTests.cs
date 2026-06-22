using Microsoft.Extensions.Logging.Abstractions;

namespace CursedThemePark.Tests;

public class CursedCheckoutServiceTests
{
    private readonly CursedCheckoutService _service = new(NullLogger<CursedCheckoutService>.Instance);
    private readonly ScenarioCatalog _scenarioCatalog = new();

    [Fact]
    public async Task CheckoutAsync_ReturnsReceipt_ForHappyPath()
    {
        var request = _scenarioCatalog.Get("happy-path").Request;

        var receipt = await _service.CheckoutAsync(request);

        Assert.Equal("Starlight Coaster", receipt.RideName);
        Assert.Equal("Avery", receipt.GuestName);
        Assert.True(receipt.Total > 0);
    }

    [Fact]
    public void ScenarioCatalog_ContainsHappyPathAndFiveCursedRepros()
    {
        var scenarios = _scenarioCatalog.GetAll();

        Assert.Equal(6, scenarios.Count);
    }

    [Fact]
    public async Task GhostTrainVipScenario_ReproducesNullReferenceException()
    {
        var request = _scenarioCatalog.Get("ghost-train-vip").Request;

        await Assert.ThrowsAsync<NullReferenceException>(() => _service.CheckoutAsync(request));
    }

    [Fact]
    public async Task ThunderLoopWeatherScenario_ReproducesHttpRequestException()
    {
        var request = _scenarioCatalog.Get("thunder-loop-weather").Request;

        await Assert.ThrowsAsync<HttpRequestException>(() => _service.CheckoutAsync(request));
    }

    [Fact]
    public async Task HauntedPopcornScenario_ReproducesKeyNotFoundException()
    {
        var request = _scenarioCatalog.Get("haunted-popcorn").Request;

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.CheckoutAsync(request));
    }

    [Fact]
    public async Task ZeroGroupDiscountScenario_ReproducesDivideByZeroException()
    {
        var request = _scenarioCatalog.Get("zero-group-discount").Request;

        await Assert.ThrowsAsync<DivideByZeroException>(() => _service.CheckoutAsync(request));
    }

    [Fact]
    public async Task EncoreDispatchScenario_ReproducesInvalidOperationException()
    {
        var request = _scenarioCatalog.Get("encore-dispatch").Request;

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.CheckoutAsync(request));
    }
}
