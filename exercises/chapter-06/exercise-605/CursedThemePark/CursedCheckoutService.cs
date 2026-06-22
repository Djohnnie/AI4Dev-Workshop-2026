using System.Diagnostics;

namespace CursedThemePark;

public sealed class CursedCheckoutService(ILogger<CursedCheckoutService> logger)
{
    private static readonly IReadOnlyDictionary<string, Ride> Rides = new Dictionary<string, Ride>
    {
        ["ghost-train"] = new("ghost-train", "Ghost Train", 24m, false, null),
        ["thunder-loop"] = new("thunder-loop", "Thunder Loop", 31m, true, new RideOperator("Ivy Voltage")),
        ["starlight-coaster"] = new("starlight-coaster", "Starlight Coaster", 18m, false, new RideOperator("Nova Spin")),
        ["moon-drop"] = new("moon-drop", "Moon Drop", 22m, false, new RideOperator("Luna Launch"))
    };

    private static readonly IReadOnlyDictionary<string, decimal> SnackPrices = new Dictionary<string, decimal>
    {
        ["funnel-cake"] = 7.50m,
        ["starlight-soda"] = 4.25m
    };

    public async Task<ParkCheckoutReceipt> CheckoutAsync(ParkCheckoutRequest request, CancellationToken cancellationToken = default)
    {
        using var activity = CheckoutTelemetry.ActivitySource.StartActivity("park.checkout", ActivityKind.Internal);
        activity?.SetTag("checkout.scenario", request.ScenarioId);
        activity?.SetTag("checkout.ride_id", request.RideId);
        activity?.SetTag("checkout.group_size", request.GroupSize);

        CheckoutTelemetry.RequestCounter.Add(1, new KeyValuePair<string, object?>("scenario", request.ScenarioId));

        try
        {
            logger.LogInformation(
                "Starting cursed checkout scenario {ScenarioId} for ride {RideId} and guest {GuestName}",
                request.ScenarioId,
                request.RideId,
                request.GuestName);

            var ride = Rides[request.RideId];

            if (request.IncludeWeatherCheck)
            {
                await VerifyWeatherAsync(ride, cancellationToken);
            }

            var total = CalculateTicketPrice(ride, request);
            total += ReserveSnack(request.SnackCode);

            if (request.IsVipNight)
            {
                var operatorName = ride.Operator!.Name;
                logger.LogInformation("VIP night ride will be hosted by {OperatorName}", operatorName);
            }

            if (request.TriggerEncoreStart)
            {
                var dispatchLog = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                StartRide(dispatchLog, ride.Id);
                StartRide(dispatchLog, ride.Id);
            }

            var receipt = new ParkCheckoutReceipt(
                $"CTP-{Guid.NewGuid():N}"[..12].ToUpperInvariant(),
                ride.Name,
                request.GuestName,
                total,
                $"{request.GuestName} is cleared for {ride.Name}.");

            logger.LogInformation(
                "Checkout completed for scenario {ScenarioId} with confirmation {ConfirmationCode}",
                request.ScenarioId,
                receipt.ConfirmationCode);

            return receipt;
        }
        catch (Exception exception)
        {
            CheckoutTelemetry.FailureCounter.Add(
                1,
                new KeyValuePair<string, object?>("scenario", request.ScenarioId),
                new KeyValuePair<string, object?>("exception", exception.GetType().Name));

            logger.LogError(
                exception,
                "Cursed checkout scenario {ScenarioId} failed for guest {GuestName}",
                request.ScenarioId,
                request.GuestName);

            throw;
        }
    }

    private static async Task VerifyWeatherAsync(Ride ride, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!ride.RequiresWeatherClearance)
        {
            return;
        }

        await Task.Delay(25, cancellationToken);
        throw new HttpRequestException($"The storm wardens could not verify weather clearance for {ride.Name}.");
    }

    private static decimal CalculateTicketPrice(Ride ride, ParkCheckoutRequest request)
    {
        if (request.UseGroupDiscount)
        {
            var hauntedDiscount = 90 / request.GroupSize;
            return ride.BasePrice * request.GroupSize - hauntedDiscount;
        }

        var groupSize = Math.Max(1, request.GroupSize);
        return ride.BasePrice * groupSize;
    }

    private static decimal ReserveSnack(string? snackCode)
    {
        if (string.IsNullOrWhiteSpace(snackCode))
        {
            return 0m;
        }

        return SnackPrices[snackCode];
    }

    private static void StartRide(ISet<string> dispatchLog, string rideId)
    {
        if (!dispatchLog.Add(rideId))
        {
            throw new InvalidOperationException($"Ride {rideId} has already been started in this checkout.");
        }
    }
}
