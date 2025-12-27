namespace Basket.Models
{
    public class ShopingCartItem
    {
        public int Quantity { get; set; }= default!;

        public string Color { get; set; }= default!;
        public int ProductId { get; set; }= default!;

        //willbe come from catlog service
        public decimal Price { get; set; }= default!;
        public string ProductName { get; set; } = default!;

    }
}
