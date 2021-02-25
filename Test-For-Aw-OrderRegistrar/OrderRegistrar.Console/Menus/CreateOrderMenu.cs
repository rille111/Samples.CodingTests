using System;
using System.Linq;
using OrderRegistrar.Domain.Concepts;
using OrderRegistrar.Domain.Models;
using OrderRegistrar.Domain.Services;

namespace OrderRegistrar.Console.Menus
{
    // Monolithic, would refactor given more time
    internal class CreateOrderMenu : IMenu
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CustomerType> _customerTypeRepository;
        private readonly OrderBuilder _orderBuilder;
        private readonly IRepository<Product> _productRepository;

        public CreateOrderMenu(IRepository<Order> orderRepository, IRepository<CustomerType> customerTypeRepository, OrderBuilder orderBuilder, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _customerTypeRepository = customerTypeRepository;
            _orderBuilder = orderBuilder;
            _productRepository = productRepository;
        }

        public MenuAction Show()
        {
            System.Console.Clear();
            System.Console.WriteLine("Create New Order");
            System.Console.WriteLine();
            System.Console.WriteLine("Choose one of the following: ");
            System.Console.WriteLine();

            foreach (var cust in _customerTypeRepository.ItemsWhere(p => true))
            {
                System.Console.WriteLine($"{cust.Id} - {cust.Description}");
            }

            System.Console.WriteLine();
            System.Console.WriteLine("M - Go to main menu");
            System.Console.WriteLine("Esc - Exit");
            System.Console.WriteLine("--- Awaiting user input ---");

            while (true)
            {
                var input = System.Console.ReadKey();

                if (TryGetCustomerType(input, out var customerType))
                {
                    _orderBuilder.Start(customerType);
                    return ChooseProduct(customerType);
                }

                switch (input.Key)
                {
                    case ConsoleKey.M:
                        return MenuAction.Main;
                    case ConsoleKey.Escape:
                        return MenuAction.Exit;
                }
            }
        }

        private MenuAction ChooseProduct(CustomerType customerType)
        {
            while (true)
            {
                System.Console.Clear();
               
                System.Console.WriteLine($"Creating order for: {customerType.Description}");
                System.Console.WriteLine();
                if (_orderBuilder.ShoppingCart.Any())
                {
                    System.Console.WriteLine("In ShoppingCart: ");
                    _orderBuilder.ShoppingCart.ForEach(p => System.Console.WriteLine($" * {p.OrderedQuantity} {p.OrderedProduct.Name}"));
                    System.Console.WriteLine();
                }

                System.Console.WriteLine("Choose product: ");
                System.Console.WriteLine();

                foreach (var prod in _productRepository.ItemsWhere(p => true))
                {
                    System.Console.WriteLine($"{prod.Id} - {prod.Name}, ListPrice: [{prod.UnitListPrice}]");
                }
                System.Console.WriteLine();
                System.Console.WriteLine("S - Save and go back");
                System.Console.WriteLine("M - Cancel & go to main menu");
                System.Console.WriteLine("Esc - Exit");
                System.Console.WriteLine("--- Awaiting user input ---");

                var input = System.Console.ReadKey();

                if (TryGetProduct(input, out var product))
                {
                    var qty = AskHowMany(product);
                    _orderBuilder.AddOrderItem(product, qty);
                }

                switch (input.Key)
                {
                    case ConsoleKey.S:
                        {
                            // Save if we have any products
                            var order = _orderBuilder.Finish();
                            _orderRepository.Create(order);
                            return MenuAction.CreateOrder;
                        }
                    case ConsoleKey.M:
                    {
                        _orderBuilder.Cancel();
                        return MenuAction.Main;
                    }
                        
                    case ConsoleKey.Escape:
                        return MenuAction.Exit;
                }
            }
        }

        private int AskHowMany(Product product)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine($"{product.Name} chosen. How Many? (1 - 100)");
            System.Console.WriteLine();

            while (true)
            {
                var input = System.Console.ReadLine();
                if (int.TryParse(input, out var thisMany))
                {
                    if (thisMany < 1 || thisMany > 100)
                    {
                        System.Console.WriteLine("Wrong, please enter a number between 1 and 100.");
                    }
                    else
                    {
                        // Ok
                        return thisMany;
                    }
                }
                else
                {
                    System.Console.WriteLine("Wrong, please enter a number.");
                }
            }
            
        }

        // Helpers
        private bool TryGetCustomerType(ConsoleKeyInfo input, out CustomerType customerType)
        {
            customerType = null;
            if (int.TryParse(input.KeyChar.ToString(), out var id))
            {
                customerType = _customerTypeRepository.ItemWhere(p => p.Id == id);
                return customerType != null;
            }
            return false;
        }

        private bool TryGetProduct(ConsoleKeyInfo input, out Product product)
        {
            product = null;
            if (int.TryParse(input.KeyChar.ToString(), out var id))
            {
                product = _productRepository.ItemWhere(p => p.Id == id);
                return product != null;
            }
            return false;
        }
    }
}