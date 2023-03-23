using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace LegoService.Repositories;

public class SecretsRepository : ISecretsRepository
{
    private readonly DaprClient _daprClient;

    public SecretsRepository(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<string> GetSecret(string secretName)
    {
        var secret = await _daprClient.GetSecretAsync("local-secret-store", secretName);
        return secret[secretName];
    }
}
public interface ISecretsRepository
{
    public Task<string> GetSecret(string secretName);
}