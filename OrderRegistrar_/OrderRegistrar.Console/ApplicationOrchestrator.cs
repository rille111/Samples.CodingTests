using OrderRegistrar.Console.Menus;
using OrderRegistrar.Domain.Concepts;
using OrderRegistrar.Domain.Models;
using OrderRegistrar.Domain.Services;

namespace OrderRegistrar.Console
{
    public class ApplicationOrchestrator
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CustomerType> _customerTypeRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly OrderBuilder _orderBuilder;

        public ApplicationOrchestrator(
            IRepository<Order> orderRepository, 
            IRepository<CustomerType> customerTypeRepository, 
            IRepository<Product> productRepository, 
            OrderBuilder orderBuilder)
        {
            _orderRepository = orderRepository;
            _customerTypeRepository = customerTypeRepository;
            _productRepository = productRepository;
            _orderBuilder = orderBuilder;
        }

        public void Start()
        {
            var result = PresentMenu(MenuAction.Main);

            do
            {
                result = PresentMenu(result);
            } while (result != MenuAction.Exit);

        }

        private MenuAction PresentMenu(MenuAction action)
        {
            IMenu menu = null;
            switch (action)
            {
                case MenuAction.Main:
                    menu = new MainMenu();
                    break;
                case MenuAction.CreateOrder:
                    menu = new CreateOrderMenu(_orderRepository, _customerTypeRepository, _orderBuilder, _productRepository);
                    break;
                case MenuAction.ShowOrders:
                    menu = new ShowOrdersMenu(_orderRepository);
                    break;
                case MenuAction.Exit:
                    return MenuAction.Exit;
            }

            // ReSharper disable once PossibleNullReferenceException
            return menu.Show();
        }
    }
}
