using System;
using System.Collections.Generic;
using System.Linq;

using GoodGroceries.Models;

namespace GoodGroceries.Services
{
    public class ConsoleService
    {
        private readonly BillService _billService;
        private readonly IEnumerable<Product> _products;
        private readonly IEnumerable<SpecialOffer> _specialOffers;
        private List<ConsoleOption> _groceryOptions;
        private List<ConsoleOption> _menuOptions;
        private readonly string HeaderMainMenu = "Welcome to GoodGroceries!";
        private readonly string HeaderGroceryList = "Grocery List";
        private readonly string HeaderCheckoutBill = "Your final bill";

        public ConsoleService(BillService billService, IEnumerable<Product> products,
            IEnumerable<SpecialOffer> specialOffers)
        {
            _billService = billService;
            _products = products;
            _specialOffers = specialOffers;
        }

        public void GetInteractiveInput()
        {
            // Create main menu option choices
            _menuOptions = new List<ConsoleOption>
            {
                new("Add groceries", () => GetMenuInput(HeaderGroceryList, _groceryOptions)),
                new("Show bill", () => ShowBill(HeaderCheckoutBill)),
                new("Quit", () => Environment.Exit(0))
            };

            // Create shopping list items
            _groceryOptions = _products.Select(product =>
            {
                return new ConsoleOption(
                    $"{product.Name} ({product.Price:C} each)",
                    () => GetQuantityInput(product)
                );
            }).ToList();
            _groceryOptions.Add(new ConsoleOption("Go back", () => GetMenuInput(HeaderMainMenu, _menuOptions)));

            // Show the main menu
            GetMenuInput(HeaderMainMenu, _menuOptions);
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
        private void ShowBill(string header)
        {
            Console.Clear();
            Console.WriteLine($"\n{header}");

            // Totals for each item
            Console.WriteLine(new String('=', 60));
            Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", "Product", "Price", "Quantity", "Total");
            foreach (var basketItem in _billService.Basket)
            {
                var itemTotalPrice = $"{basketItem.TotalPrice:C}";
                Console.WriteLine("{0,-15} {1,-15} {2,-15} {3,-15}", basketItem.Product.Name,
                    $"{basketItem.Product.Price:C}",
                    basketItem.Quantity, itemTotalPrice);
            }

            // Subtotal of all before offers
            var totalBeforeOffers = _billService.GetTotalBeforeOffers();
            Console.WriteLine(new String('-', 60));
            Console.WriteLine("{0,60}", $"Subtotal: {totalBeforeOffers:C}");

            // Savings for each offer
            foreach (SpecialOffer specialOffer in _specialOffers)
            {
                var savings = _billService.GetTotalDiscountForOffer(specialOffer);
                if (savings > 0)
                {
                    Console.WriteLine("{0,-53} {1,-7}", specialOffer.Description, $"{-savings:C}");
                }
            }

            // Total for all savings
            var totalSavings = _billService.GetTotalSavings();
            Console.WriteLine("{0,60}", $"Total savings: {-totalSavings:C}");

            // Total
            var totalAfterDiscounts = totalBeforeOffers - totalSavings;
            Console.WriteLine(new String('-', 60));
            Console.WriteLine("{0,60}", $"Total to pay: {totalAfterDiscounts:C}");
            Console.WriteLine(new String('=', 60));

            // Return to main menu
            Console.WriteLine("Press enter to return");
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();
                Console.WriteLine("\nPress enter to return");
            } while (keyInfo.Key != ConsoleKey.Enter);

            GetMenuInput(HeaderMainMenu, _menuOptions);
        }

        /// <summary>
        /// Get the input for the product quantity
        /// </summary>
        void GetQuantityInput(Product product)
        {
            Console.Clear();
            Console.WriteLine("\nHow many would you like to purchase?");

            int quantity;
            while (!int.TryParse(Console.ReadLine(), out quantity))
            {
                Console.WriteLine("Please enter a number: ");
            }

            _billService.UpdateBasketItemQuantity(product, quantity);

            // Back to main menu
            GetMenuInput(HeaderMainMenu, _menuOptions);
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