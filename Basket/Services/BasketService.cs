using Basket.ApiClients;
using Basket.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache,CatalogApiClient apiClient)
    {
        public async Task<ShoppingCart?> GetBasketAsync(string userName)
        {
            var basket = await cache.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null:JsonSerializer.Deserialize<ShoppingCart>(basket);
        }
       
        public async Task UpdateBasketAsync(ShoppingCart basket)
        {
            // Before update(Add/remove Item) into SC, we should call Catalog ms GetProductById method
            // Get latest product information and set Price and ProductName when adding item into SC
            foreach (var item in basket.Items)
            {
                var product = await apiClient.GetProductsByIdAsync(item.ProductId);
                item.Price = product.Price;
                item.ProductName = product.Name;
            }
            var serializedBasket = JsonSerializer.Serialize(basket);
            await cache.SetStringAsync(basket.UserName, serializedBasket);
        }
        public async Task DeleteBasketAsync(string userName)
        {
            await cache.RemoveAsync(userName);
        }
    }
}
