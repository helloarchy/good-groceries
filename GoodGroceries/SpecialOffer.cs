namespace GoodGroceries
{
    public class SpecialOffer
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercentage { get; set; }

        public SpecialOffer(Product product, int quantity, decimal discountPercentage)
        {
            Product = product;
            Quantity = quantity;
            DiscountPercentage = discountPercentage;
        }
    }
}