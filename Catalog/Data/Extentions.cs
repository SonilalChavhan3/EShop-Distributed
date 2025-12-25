using Catalog.Models;

namespace Catalog.Data
{
    public static class Extentions
    {
        public static void UseMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProductDbContext>();
            context.Database.Migrate();
            DataSeeder.Seed(context);
        }
    }

    public static class DataSeeder
    {
        public static void Seed(ProductDbContext context)
        {
            if (context.Products.Any())
                return;

            context.Products.AddRange(Products);
            context.SaveChanges();


        }

        public static readonly List<Product> Products = new()
        {
            new Product
            {
                Name = "Laptop",
                Description = "High performance laptop",
                Price = 75000,
                ImageUrl = "images/products/laptop.png",
                Quantity = 10,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(
                            new DateTime(2025, 1, 1),
                             DateTimeKind.Utc
                                                )
            },
            new Product
            {
                Name = "Smartphone",
                Description = "Android smartphone",
                Price = 25000,
                ImageUrl = "images/products/smartphone.png",
                Quantity = 25,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(
            new DateTime(2025, 1, 1),
            DateTimeKind.Utc
        )
            },
            new Product
            {
                Name = "Tablet",
                Description = "10-inch Android tablet",
                Price = 18000,
                ImageUrl = "images/products/tablet.png",
                Quantity = 15,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "Wireless Mouse",
                Description = "Ergonomic wireless mouse",
                Price = 1200,
                ImageUrl = "images/products/mouse.png",
                Quantity = 50,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "Keyboard",
                Description = "Mechanical keyboard",
                Price = 3500,
                ImageUrl = "images/products/keyboard.png",
                Quantity = 40,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "Monitor",
                Description = "24-inch LED monitor",
                Price = 15000,
                ImageUrl = "images/products/monitor.png",
                Quantity = 20,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "Printer",
                Description = "All-in-one inkjet printer",
                Price = 9000,
                ImageUrl = "images/products/printer.png",
                Quantity = 8,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "External Hard Drive",
                Description = "1TB USB 3.0 hard drive",
                Price = 6000,
                ImageUrl = "images/products/harddrive.png",
                Quantity = 30,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "Webcam",
                Description = "Full HD webcam",
                Price = 4000,
                ImageUrl = "images/products/webcam.png",
                Quantity = 18,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            },
            new Product
            {
                Name = "Bluetooth Speaker",
                Description = "Portable Bluetooth speaker",
                Price = 5000,
                ImageUrl = "images/products/speaker.png",
                Quantity = 22,
                IsActive = true,
                CreatedAt = DateTime.SpecifyKind(new DateTime(2025, 1, 1), DateTimeKind.Utc)
            }
        };
    }

}
