using BasketService.Models;
using BasketService.Repositories;
using BasketService.services;
using BasketService.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5010").UseGrpcEndpoint("http://localhost:60001"));
builder.Services.AddValidatorsFromAssemblyContaining<BasketValidator>();
builder.Services.AddTransient<ISetBasketRepository, SetBasketRepository>();
builder.Services.AddTransient<IRedisBasketService, RedisBasketService>();
var app = builder.Build();

app.MapGet("/", () => "Hello from basketservice!");

app.MapPost("/basket/", async (IValidator<BasketItem> validator, IRedisBasketService service, BasketItem basket) =>
{
    var validationResult = validator.Validate(basket);
    if (!validationResult.IsValid)
    {
        var errors = validationResult.Errors.Select(e => e.ErrorMessage);
        return Results.BadRequest(errors);
    }

    var res = await service.AddToBasketAsync(basket);

    return Results.Created("/basket", res);
});

app.MapGet("/basket/{email}", async (IRedisBasketService service, string email) =>
{
    try
    {
        var res = await service.GetBasketAsync(email);
        return res == null ? Results.NotFound() : Results.Ok(res);
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.StatusCode(500);
    }
});

app.MapDelete("/basket/{email}", async (IRedisBasketService service, string email) =>
{
    try
    {
        await service.DeleteBasketAsync(email);
        return Results.NoContent();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        return Results.StatusCode(500);
    }
});


app.Run();