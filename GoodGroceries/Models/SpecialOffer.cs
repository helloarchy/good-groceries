namespace GoodGroceries.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SpecialOffer
    {
        public string Description { get; set; }
        public Product RequiredProduct { get; set; }
        public int RequiredQuantity { get; set; }
        /// <summary>
        /// The number by which the total price is divided to give the total discount amount
        /// </summary>
        public decimal DiscountMultiplier { get; set; }
        public Product DiscountedProduct { get; set; }
    }
}