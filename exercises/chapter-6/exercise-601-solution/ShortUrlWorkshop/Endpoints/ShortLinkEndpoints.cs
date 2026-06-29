using ShortUrlWorkshop.Domain;
using ShortUrlWorkshop.Models;
using ShortUrlWorkshop.Services;

namespace ShortUrlWorkshop.Endpoints;

public static class ShortLinkEndpoints
{
    public static void MapShortLinkEndpoints(this WebApplication app)
    {
        var links = app.MapGroup("/api/links");

        links.MapPost("/", CreateShortLinkAsync);
        links.MapGet("/", ListShortLinksAsync);
        links.MapGet("/{code}/stats", GetShortLinkStatsAsync);

        app.MapGet("/{code}", ResolveShortLinkAsync);
    }

    private static async Task<IResult> CreateShortLinkAsync(
        CreateShortLinkRequest request,
        IShortLinkService shortLinkService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var result = await shortLinkService.CreateAsync(request, cancellationToken);

        return result.Status switch
        {
            CreateShortLinkStatus.Success => Results.Created(
                $"/{result.Link!.Code}",
                ToResponse(result.Link, httpContext)),
            CreateShortLinkStatus.ValidationFailed => Results.ValidationProblem(result.Errors!),
            CreateShortLinkStatus.Conflict => Results.Conflict(new { message = result.Message }),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    private static async Task<IResult> ResolveShortLinkAsync(
        string code,
        IShortLinkService shortLinkService,
        CancellationToken cancellationToken)
    {
        var result = await shortLinkService.ResolveAsync(code, cancellationToken);

        return result.Status switch
        {
            ResolveShortLinkStatus.Success => Results.Redirect(result.DestinationUrl!),
            ResolveShortLinkStatus.NotFound => Results.NotFound(new { message = "Short code not found." }),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }

    private static async Task<IResult> GetShortLinkStatsAsync(
        string code,
        IShortLinkService shortLinkService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var shortLink = await shortLinkService.GetByCodeAsync(code, cancellationToken);
        return shortLink is null
            ? Results.NotFound(new { message = "Short code not found." })
            : Results.Ok(ToResponse(shortLink, httpContext));
    }

    private static async Task<IResult> ListShortLinksAsync(
        IShortLinkService shortLinkService,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var links = await shortLinkService.ListAsync(cancellationToken);
        return Results.Ok(links.Select(link => ToResponse(link, httpContext)));
    }

    private static ShortLinkDetailsResponse ToResponse(ShortLink shortLink, HttpContext httpContext)
    {
        var scheme = string.IsNullOrWhiteSpace(httpContext.Request.Scheme) ? "http" : httpContext.Request.Scheme;
        var host = httpContext.Request.Host.HasValue ? httpContext.Request.Host.Value : "localhost";
        var shortUrl = $"{scheme}://{host}/{shortLink.Code}";

        return new ShortLinkDetailsResponse(
            shortLink.Code,
            shortUrl,
            shortLink.DestinationUrl,
            shortLink.CreatedAtUtc,
            shortLink.RedirectCount,
            shortLink.LastRedirectedAtUtc);
    }
}
