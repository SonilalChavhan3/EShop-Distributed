using Basket.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache)
    {
        public async Task<ShopingCart?> GetBasketAsync(string userName)
        {
            var basket = await cache.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null:JsonSerializer.Deserialize<ShopingCart>(basket);
        }
       
        public async Task UpdateBasketAsync(ShopingCart basket)
        {
            var serializedBasket = JsonSerializer.Serialize(basket);
            await cache.SetStringAsync(basket.UserName, serializedBasket);
        }
        public async Task DeleteBasketAsync(string userName)
        {
            await cache.RemoveAsync(userName);
        }
    }
}
