using System.Collections.Generic;

namespace GoodGroceries.Models
{
    public class Bill
    {
        public List<Product> Products { get; set; }
        public List<SpecialOffer> SpecialOffers { get; set; }
    }
}