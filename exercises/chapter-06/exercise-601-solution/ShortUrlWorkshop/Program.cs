using ShortUrlWorkshop.Endpoints;
using ShortUrlWorkshop.Repositories;
using ShortUrlWorkshop.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IShortLinkRepository, InMemoryShortLinkRepository>();
builder.Services.AddSingleton<IShortCodeGenerator, ShortCodeGenerator>();
builder.Services.AddSingleton<IShortLinkService, ShortLinkService>();

var app = builder.Build();

app.MapShortLinkEndpoints();

app.Run();

public partial class Program;
