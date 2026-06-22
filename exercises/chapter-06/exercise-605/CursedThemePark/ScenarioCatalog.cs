namespace CursedThemePark;

public sealed class ScenarioCatalog
{
    private readonly IReadOnlyDictionary<string, ReproScenario> _scenarios = new Dictionary<string, ReproScenario>
    {
        ["happy-path"] = new(
            "happy-path",
            "Happy Path",
            "A guest buys two Starlight Coaster tickets and everything works.",
            "None",
            new ParkCheckoutRequest(
                "happy-path",
                "starlight-coaster",
                "Avery",
                2,
                "funnel-cake",
                false,
                false,
                false,
                false)),
        ["ghost-train-vip"] = new(
            "ghost-train-vip",
            "Ghost Train VIP",
            "VIP guests cannot complete the haunted midnight ride booking.",
            "NullReferenceException",
            new ParkCheckoutRequest(
                "ghost-train-vip",
                "ghost-train",
                "Morgan",
                2,
                null,
                true,
                false,
                false,
                false)),
        ["thunder-loop-weather"] = new(
            "thunder-loop-weather",
            "Thunder Loop Weather",
            "The storm-night checkout fails right when the safety check should happen.",
            "HttpRequestException",
            new ParkCheckoutRequest(
                "thunder-loop-weather",
                "thunder-loop",
                "Sky",
                3,
                null,
                false,
                true,
                false,
                false)),
        ["haunted-popcorn"] = new(
            "haunted-popcorn",
            "Haunted Popcorn",
            "Adding haunted popcorn to the order crashes the haunted concessions flow.",
            "KeyNotFoundException",
            new ParkCheckoutRequest(
                "haunted-popcorn",
                "ghost-train",
                "Jamie",
                2,
                "haunted-popcorn",
                false,
                false,
                false,
                false)),
        ["zero-group-discount"] = new(
            "zero-group-discount",
            "Zero Group Discount",
            "A weird group-discount path crashes when the guest count ends up as zero.",
            "DivideByZeroException",
            new ParkCheckoutRequest(
                "zero-group-discount",
                "starlight-coaster",
                "Taylor",
                0,
                null,
                false,
                false,
                true,
                false)),
        ["encore-dispatch"] = new(
            "encore-dispatch",
            "Encore Dispatch",
            "The encore start button seems to start the same ride twice during booking.",
            "InvalidOperationException",
            new ParkCheckoutRequest(
                "encore-dispatch",
                "moon-drop",
                "Jordan",
                4,
                "starlight-soda",
                false,
                false,
                false,
                true))
    };

    public IReadOnlyCollection<ReproScenario> GetAll() => _scenarios.Values.ToArray();

    public ReproScenario Get(string scenarioId)
    {
        if (_scenarios.TryGetValue(scenarioId, out var scenario))
        {
            return scenario;
        }

        throw new KeyNotFoundException($"Unknown cursed scenario '{scenarioId}'.");
    }
}
