namespace GoodGroceries.Models
{
    public class BasketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}