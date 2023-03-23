using LegoService.Configuration;
using LegoService.Repositories;

namespace LegoService.Services;

public class SecretsService : ISecretsService
{
    private readonly ISecretsRepository _secretsRepository;

    public SecretsService(ISecretsRepository secretsRepository)
    {
        _secretsRepository = secretsRepository;
    }

    public async Task<string> GetSecret(string secretName)
    {
        return await _secretsRepository.GetSecret(secretName);
    }
    
    public async Task<DatabaseSettings> GetDatabaseSettings()
    {
        var settings = new DatabaseSettings()
        {
            ConnectionString = await GetSecret("ConnectionString"),
            DatabaseName = await GetSecret("DatabaseName"),
            SetCollectionName = await GetSecret("SetCollectionName"),
            ThemeCollectionName = await GetSecret("ThemeCollectionName")
        };
        return settings;
    }
}

public interface ISecretsService
{
    Task<string> GetSecret(string secretName);
    Task<DatabaseSettings> GetDatabaseSettings();
}
