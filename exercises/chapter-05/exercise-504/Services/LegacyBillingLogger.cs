namespace PromptPatternsPlayground.Services;

public sealed class LegacyBillingLogger
{
    public string Format(string invoiceId, string status) =>
        "Billing entry " + invoiceId + " moved to " + status;
}
