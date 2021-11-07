namespace GoodGroceries.Models
{
    public class SpecialOffer
    {
        public string Description { get; set; }

        /// <summary>
        /// The item that must be purchased for this offer to apply
        /// </summary>
        public Product RequiredProduct { get; set; }

        /// <summary>
        /// The number of required products needed for the offer to apply
        /// </summary>
        public int RequiredQuantity { get; set; }

        /// <summary>
        /// The number by which the total price is multiplied to give the total discount amount
        /// </summary>
        public decimal DiscountMultiplier { get; set; }

        /// <summary>
        /// The item the discount is being applied to (not always the same product)
        /// For example, buy one soup, get a bread half price.
        /// </summary>
        public Product DiscountedProduct { get; set; }
    }
}