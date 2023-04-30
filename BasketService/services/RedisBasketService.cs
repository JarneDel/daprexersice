using BasketService.Models;
using BasketService.Repositories;

namespace BasketService.services;

public class RedisBasketService: IRedisBasketService
{
    
    private readonly ISetBasketRepository _setBasketRepository;
    
    public RedisBasketService(ISetBasketRepository setBasketRepository)
    {
        _setBasketRepository = setBasketRepository;
        
    }
    public async Task<CustomerBasket?> GetBasketAsync(string email) => await _setBasketRepository.GetBasketAsync(email);
    

    public async Task<CustomerBasket> AddToBasketAsync(BasketItem basketItem) => await _setBasketRepository.AddToBasketAsync(basketItem);

    public async Task DeleteBasketAsync(string email) => await _setBasketRepository.DeleteBasketAsync(email);
}

public interface IRedisBasketService
{
    Task<CustomerBasket?> GetBasketAsync(string email);
    Task<CustomerBasket> AddToBasketAsync(BasketItem basket);
    Task DeleteBasketAsync(string email);
}