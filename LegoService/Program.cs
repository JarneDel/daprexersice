using System.Diagnostics;
using LegoService.Configuration;
using LegoService.DataContext;
using LegoService.Repositories;
using LegoService.Services;

Debugger.Launch();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5010").UseGrpcEndpoint("http://localhost:60001"));
builder.Services.AddTransient<ISecretsRepository, SecretsRepository>();
builder.Services.AddTransient<ISecretsService, SecretsService>();
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ILegoRepository, LegoRepository>();
builder.Services.AddTransient<ICollectionService, CollectionService>();
var app = builder.Build();

app.MapGet("/", () => "Hello from LegoService!");

app.MapGet("/themes", async (ICollectionService service) => await service.GetThemes());
app.MapGet("/sets", async (ICollectionService service) => await service.GetSets());
app.MapGet("/sets/{id}", async (ICollectionService service, string id) => await service.GetSet(id));
app.MapGet("/sets/theme/{themeId}", async (ICollectionService service, string themeId) => await service.GetSetsForTheme(themeId));

app.MapGet("/createtestdata", async (ICollectionService service) => await service.InsertTestData());


app.Run();