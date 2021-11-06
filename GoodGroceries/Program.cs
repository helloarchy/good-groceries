using System;

namespace GoodGroceries
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Initialise products */
            var BREAD = new Product()
            {
                Name = "Bread",
                Price = 1.10M
            };

            var MILK = new Product()
            {
                Name = "Milk",
                Price = 0.50M
            };

            var CHEESE = new Product()
            {
                Name = "Cheese",
                Price = 0.90M
            };

            var SOUP = new Product()
            {
                Name = "Soup",
                Price = 0.60M
            };
            
            

        }
    }
}