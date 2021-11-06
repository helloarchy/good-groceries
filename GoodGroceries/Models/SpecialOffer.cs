namespace GoodGroceries.Models
{
    public class SpecialOffer
    {
        public string Description { get; set; }
        public Product RequiredProduct { get; set; }
        public int RequiredQuantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public Product DiscountedProduct { get; set; }
    }
}