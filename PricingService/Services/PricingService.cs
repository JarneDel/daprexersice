using Microsoft.AspNetCore.Mvc;
using Dapr.Client;
using PricingService.models;

namespace PricingService.Services;

public class PricingService : IPricingService
{
    private DaprClient _daprClient;
    
    public PricingService(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }
    
    
    public async Task<decimal> GetPriceAsync(List<BasketItem> basketItems)
    {
        var totalPrice = 0m;
        foreach (var basketItem in basketItems)
        {
            var requestMessage =  _daprClient.CreateInvokeMethodRequest<string>(HttpMethod.Get, "CatalogService", $"sets/{basketItem.LegoSetId}", null);
            var response =  await _daprClient.InvokeMethodAsync<LegoSet>(requestMessage);
            if (response != null)
            {
                totalPrice += response.Price * basketItem.Quantity;
            }
        }
        return totalPrice;
    }
}
public interface IPricingService
{
    Task<decimal> GetPriceAsync(List<BasketItem> basketItems);
}