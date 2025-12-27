using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Catalog.Services
{
    public class ProductService(ProductDbContext context, IBus bus)
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task CreateProductAsync(Product product)
        {
            context.Products.Add(product);
            await context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product updatedProduct, Product inputProduct)
        {
            //if prie has changed, raise ProductPriceChanged intigration event 
            if (updatedProduct.Price != inputProduct.Price)
            {
                var intigrationEvent = new ProductPriceCnagedIntegrationEvent
                {
                    ProductId = updatedProduct.Id,
                    Name = inputProduct.Name,
                    Description = inputProduct.Description,
                    Price = inputProduct.Price,
                    ImageUrl = inputProduct.ImageUrl
                };
                await bus.Publish(intigrationEvent);
            }
            updatedProduct.Name = inputProduct.Name;
            updatedProduct.Description = inputProduct.Description;
            updatedProduct.Price = inputProduct.Price;
            updatedProduct.ImageUrl = inputProduct.ImageUrl;
            updatedProduct.Quantity = inputProduct.Quantity;
            updatedProduct.IsActive = inputProduct.IsActive;

            context.Products.Update(updatedProduct);
            await context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(Product product)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }
}
