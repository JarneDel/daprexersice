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
        return await _daprClient.GetStateAsync<CustomerBasket>("statestore", email);
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
            await _daprClient.SaveStateAsync("statestore", basket.Email, basket);
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
        await _daprClient.DeleteStateAsync("statestore", email);
    }
}

public interface ISetBasketRepository
{
    Task<CustomerBasket?> GetBasketAsync(string email);
    Task<CustomerBasket> AddToBasketAsync(BasketItem basketItem);
    Task DeleteBasketAsync(string email);
    
}