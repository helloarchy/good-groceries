using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using GoodGroceries.Models;

namespace GoodGroceries.Services
{
    public class ConsoleService
    {
        private static List<ConsoleOption> _groceryOptions;
        private static List<ConsoleOption> _menuOptions;
        private static List<BasketItem> _basket = new();
        private static string _headerMainMenu = "Welcome to GoodGroceries!";
        private static string _headerGroceryList = "Grocery List";
        private static string _headerCheckoutBill = "Your final bill";

        public void GetInteractiveInput(IEnumerable<Product> products, IEnumerable<SpecialOffer> specialOffers)
        {
            // Create main menu
            _menuOptions = new List<ConsoleOption>
            {
                new("Add groceries", () => GetMenuInput(_headerGroceryList, _groceryOptions)),
                new("Show bill", () => ShowBill(specialOffers)),
                new("Quit", () => Environment.Exit(0))
            };

            // Create shopping list items
            _groceryOptions = products.Select(product =>
            {
                _basket.Add(new BasketItem { Product = product, Quantity = 0 });
                return new ConsoleOption(
                    $"{product.Name} ({product.Price:C} each)",
                    () => GetQuantityInput(product)
                );
            }).ToList();
            _groceryOptions.Add(new ConsoleOption("Go back", () => GetMenuInput(_headerMainMenu, _menuOptions)));

            // Show the main menu
            GetMenuInput(_headerMainMenu, _menuOptions);
        }

        private void GetMenuInput(string header, List<ConsoleOption> options)
        {
            WriteMenu(header, options, options[0]);

            // Store key info in here
            ConsoleKeyInfo keyInfo;
            var index = 0;
            do
            {
                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        {
                            if (index + 1 < options.Count)
                            {
                                index++;
                                WriteMenu(header, options, options[index]);
                            }

                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (index - 1 >= 0)
                            {
                                index--;
                                WriteMenu(header, options, options[index]);
                            }

                            break;
                        }
                    case ConsoleKey.Enter:
                        options[index].Selected.Invoke();
                        index = 0;
                        break;
                }
            } while (keyInfo.Key != ConsoleKey.X);

            Console.ReadKey();

            // Return to main menu
            WriteMenu(header, options, options[0]);
        }

        /**
         * Show the bill, and handle input
         */
        private void ShowBill(IEnumerable<SpecialOffer> specialOffers)
        {
            IEnumerable<SpecialOffer> offers = specialOffers.ToList();
            var billService = new BillService(_basket, offers);

            Console.Clear();

            // Totals for each item
            Console.WriteLine(new String('=', 60));
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "Product", "Price", "Quantity", "Total");
            foreach (var basketItem in _basket.Where(item => item.Quantity > 0))
            {
                var itemTotalPrice = $"{BillService.GetTotalForItem(basketItem):C}";
                Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", basketItem.Product.Name,
                    $"{basketItem.Product.Price:C}",
                    basketItem.Quantity, itemTotalPrice);
            }

            // Subtotal of all before offers
            Console.WriteLine(new String('-', 60));
            Console.WriteLine("{0,60}", $"Subtotal: {billService.GetTotalBeforeOffers():C}");
            
            // Savings for each offer
            foreach (SpecialOffer specialOffer in offers)
            {
                var savings = billService.GetOfferSavings(specialOffer);
                if (savings > 0)
                {
                    Console.WriteLine("{0,-50} {1,10}", specialOffer.Description, $"{-savings:C}");
                }
            }
            
            // Total for all savings
            Console.WriteLine("{0,60}", $"Total savings: {-billService.GetTotalSavings():C}");
            
            // Total
            Console.WriteLine(new String('-', 60));
            Console.WriteLine("{0,60}", $"Total to pay: {billService.GetTotalAfterSavings():C}");
            Console.WriteLine(new String('=', 60));
            
            // Return to main menu
            Console.WriteLine("Press enter to return");
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                Console.WriteLine("\nPress enter to return");
            } while (keyInfo.Key != ConsoleKey.Enter);
            GetMenuInput(_headerMainMenu, _menuOptions);
        }

        // Get the input for the product quantity
        void GetQuantityInput(Product product)
        {
            int quantity;
            Console.Clear();
            Console.WriteLine("\nHow many would you like to purchase?");
            while (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Please enter a number: ");
            }

            var basketItem = _basket.Find(basketItem => basketItem.Product == product);
            if (basketItem != null)
            {
                basketItem.Quantity = quantity;
            }

            // Back to main menu
            GetMenuInput(_headerMainMenu, _menuOptions);
        }

        static void WriteMenu(string header, List<ConsoleOption> options, ConsoleOption selectedOption)
        {
            Console.Clear();
            Console.WriteLine($"\n{header}");

            foreach (var option in options)
            {
                Console.Write(option == selectedOption ? "[x] " : "[ ] ");
                Console.WriteLine(option.Name);
            }
        }
    }
}