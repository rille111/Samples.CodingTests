using System;
using OrderRegistrar.Domain.Concepts;
using OrderRegistrar.Domain.Models;

namespace OrderRegistrar.Console.Menus
{
    internal class ShowOrdersMenu : IMenu
    {
        private readonly IRepository<Order> _orderRepository;

        public ShowOrdersMenu(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public MenuAction Show()
        {
            System.Console.Clear();
            System.Console.WriteLine("Showing Orders");
            System.Console.WriteLine($"----------------------------------");
            foreach (var order in _orderRepository.ItemsWhere(p => true))
            {
                PresentOrder(order);
            }
            
            System.Console.WriteLine();
            System.Console.WriteLine("M - Main Menu");
            System.Console.WriteLine("Esc - Exit");
            System.Console.WriteLine("--- Awaiting user input ---");

            while (true)
            {
                var input = System.Console.ReadKey();

                switch (input.Key)
                {
                    case ConsoleKey.M:
                        return MenuAction.Main;
                    case ConsoleKey.Escape:
                        return MenuAction.Exit;
                }
            }
        }

        private void PresentOrder(Order order)
        {
            System.Console.WriteLine($"OrderNr: {order.Number} ({order.CustomerType.Description})");
            foreach (var item in order.Items)
            {
                System.Console.WriteLine($"* {item.OrderedProduct.Name}");
                System.Console.WriteLine($"  * Price/Item:    {item.UnitCustomerPrice} (your price)");
                if (item.UnitAppliedDiscount > 0)
                    System.Console.WriteLine($"  * Discount/Item: {item.UnitAppliedDiscount}");
                System.Console.WriteLine($"  * Quantity:      {item.OrderedQuantity}");
                if (item.QuantityGivenAway > 0)
                    System.Console.WriteLine($"  * GivenAway:     {item.QuantityGivenAway}");
                if (item.SumOfAllSavings > 0)
                    System.Console.WriteLine($"  * Sum Savings:   {item.SumOfAllSavings}");
                System.Console.WriteLine($"  * Total Price:   {item.SumCustomerPrice}");
            }
            System.Console.WriteLine();
            System.Console.WriteLine($"  Sum. Total: {order.TotalPrice}");
            System.Console.WriteLine($"----------------------------------");

        }
    }
}