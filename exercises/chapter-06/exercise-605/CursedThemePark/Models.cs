namespace CursedThemePark;

public sealed record ParkCheckoutRequest(
    string ScenarioId,
    string RideId,
    string GuestName,
    int GroupSize,
    string? SnackCode,
    bool IsVipNight,
    bool IncludeWeatherCheck,
    bool UseGroupDiscount,
    bool TriggerEncoreStart);

public sealed record ParkCheckoutReceipt(
    string ConfirmationCode,
    string RideName,
    string GuestName,
    decimal Total,
    string Message);

public sealed record ReproScenario(
    string Id,
    string Title,
    string GuestReport,
    string ExpectedException,
    ParkCheckoutRequest Request);

public sealed record RideOperator(string Name);

public sealed record Ride(
    string Id,
    string Name,
    decimal BasePrice,
    bool RequiresWeatherClearance,
    RideOperator? Operator);
