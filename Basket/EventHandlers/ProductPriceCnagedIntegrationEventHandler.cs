using Basket.Services;
using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Basket.EventHandlers
{
    public class ProductPriceCnagedIntegrationEventHandler(BasketService basketService)
        : IConsumer<ProductPriceCnagedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<ProductPriceCnagedIntegrationEvent> context)
        {
           await basketService.UpdateBasketsItemProductPrices(
                context.Message.ProductId,
                context.Message.Price);
        }
    }
}
