using PromptPatternsPlayground.Services;

namespace PromptPatternsPlayground.Tests;

public sealed class SecurityHeadersPolicyTests
{
    [Fact]
    public void BuildDefaultHeaders_ContainsContentSecurityPolicy()
    {
        var policy = new SecurityHeadersPolicy();

        var headers = policy.BuildDefaultHeaders();

        Assert.True(headers.ContainsKey("Content-Security-Policy"));
    }

    [Fact]
    public void BuildDefaultHeaders_UsesDenyForFrames()
    {
        var policy = new SecurityHeadersPolicy();

        var headers = policy.BuildDefaultHeaders();

        Assert.Equal("DENY", headers["X-Frame-Options"]);
    }
}
