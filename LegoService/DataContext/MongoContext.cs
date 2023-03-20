using LegoService.Configuration;
using LegoService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LegoService.DataContext;

public class MongoContext: IMongoContext
{
    
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    private readonly DatabaseSettings _settings;
    
    
    public IMongoClient Client => _client;
    public IMongoDatabase Database => _database;
    
    public MongoContext(IOptions<DatabaseSettings> dbOptions)
    {
        _settings = dbOptions.Value;
        _client = new MongoClient(_settings.ConnectionString);
        _database = _client.GetDatabase(_settings.DatabaseName);
    }
    
    public IMongoCollection<LegoSet> SetCollection => _database.GetCollection<LegoSet>(_settings.SetCollectionName);
    public IMongoCollection<LegoTheme> ThemeCollection => _database.GetCollection<LegoTheme>(_settings.ThemeCollectionName );
    
    
}

public interface IMongoContext 
{
    public IMongoDatabase Database { get; }
    public IMongoCollection<LegoSet> SetCollection { get; }
    public IMongoCollection<LegoTheme> ThemeCollection {get;}
}