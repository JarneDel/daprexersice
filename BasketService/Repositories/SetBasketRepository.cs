using System.Text.Json;
using BasketService.Models;
using Dapr.Client;

namespace BasketService.Repositories;

public class SetBasketRepository : ISetBasketRepository
{
    private readonly DaprClient _daprClient;
    
    public SetBasketRepository(DaprClient daprClient)
    {
        _daprClient = daprClient;
    }

    public async Task<CustomerBasket?> GetBasketAsync(string email)
    {
        var state =  await _daprClient.GetStateAsync<CustomerBasket>("redisStore", email);
        return state;
    }
    
    public async Task<CustomerBasket> AddToBasketAsync(BasketItem basketItem)
    {
        try
        {
            var basket = new CustomerBasket();
            var response = await GetBasketAsync(basketItem.Email);
            if (response != null) basket = response;
            else
            {
                basket.Email = basketItem.Email;
                basket.Items = new List<BasketItem>();
            }

            basket.Items.Add(basketItem);
            // todo: calculate price

            var requestMessage = _daprClient.CreateInvokeMethodRequest<List<BasketItem>>(HttpMethod.Post, "PricingService", "price/", basket.Items);
            var totalPrice = await _daprClient.InvokeMethodAsync<decimal>(requestMessage);
            
            basket.TotalPrice = totalPrice;
            await _daprClient.SaveStateAsync("redisStore", basket.Email, basket);
            return basket;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task DeleteBasketAsync(string email)
    {
        await _daprClient.DeleteStateAsync("redisStore", email);
    }
}

public interface ISetBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string email);
    Task<CustomerBasket> AddToBasketAsync(BasketItem basketItem);
    Task DeleteBasketAsync(string email);
    
}