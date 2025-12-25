namespace Catalog.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Description { get; set; } = default!;

        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = default!;

        public int Quantity { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
