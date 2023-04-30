using System.Text.Json;
using PricingService.models;
using PricingService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IPricingService, PricingService.Services.PricingService>();
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5012").UseGrpcEndpoint("http://localhost:60002"));
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/price/", async (IPricingService pricingService, List<BasketItem> items) =>
{
    try
    {
        Console.WriteLine($"Getting price for {items.Count} items");
        var result = await pricingService.GetPriceAsync(items);
        // result to json
        var json = JsonSerializer.Serialize(result);
        return Results.Ok(json);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.StatusCode(500);
    }

});

app.Run();