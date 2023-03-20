using LegoService.Configuration;
using LegoService.DataContext;
using LegoService.Repositories;
using LegoService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient(opt => opt.UseHttpEndpoint("http://localhost:5010").UseGrpcEndpoint("http://localhost:60001"));
var settings = builder.Configuration.GetSection("MongoConnection");
builder.Services.Configure<DatabaseSettings>(settings);
builder.Services.AddTransient<IMongoContext, MongoContext>();
builder.Services.AddTransient<ILegoRepository, LegoRepository>();
builder.Services.AddTransient<ILegoService, LegoService.Services.LegoService>();
var app = builder.Build();

app.MapGet("/", () => "Hello from LegoService!");

app.MapGet("/themes", async (ILegoService service) => await service.GetThemes());
app.MapGet("/sets", async (ILegoService service) => await service.GetSets());
app.MapGet("/sets/{id}", async (ILegoService service, string id) => await service.GetSet(id));
app.MapGet("/sets/theme/{themeId}", async (ILegoService service, string themeId) => await service.GetSetsForTheme(themeId));

app.MapGet("/createtestdata", async (ILegoService service) => await service.InsertTestData());


app.Run();