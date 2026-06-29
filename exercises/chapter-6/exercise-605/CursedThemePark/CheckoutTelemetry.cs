using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace CursedThemePark;

public static class CheckoutTelemetry
{
    public static readonly ActivitySource ActivitySource = new("CursedThemePark.Checkout");
    public static readonly Meter Meter = new("CursedThemePark.Checkout");
    public static readonly Counter<int> RequestCounter = Meter.CreateCounter<int>("checkout.requests");
    public static readonly Counter<int> FailureCounter = Meter.CreateCounter<int>("checkout.failures");
}
