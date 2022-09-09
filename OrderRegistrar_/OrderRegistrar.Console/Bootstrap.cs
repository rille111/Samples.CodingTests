using OrderRegistrar.Domain.Concepts;
using OrderRegistrar.Domain.Models;
using OrderRegistrar.Domain.Services;
using OrderRegistrar.Infrastructure.Repository;
using SimpleInjector;

namespace OrderRegistrar.Console
{
    // ReSharper disable RedundantTypeArgumentsOfMethod
    internal class Bootstrap
    {
        public static Container ConfigureContainer()
        {
            var container = new Container();

            // Repositories
            container.Register<IRepository<Order>>(CreateOrderRepository, Lifestyle.Singleton);
            container.Register<IRepository<Product>>(CreateProductRepository, Lifestyle.Singleton);
            container.Register<IRepository<CustomerType>>(CreateCustomerTypeRepository, Lifestyle.Singleton);

            // Services
            container.Register<IDiscountCalculator, DiscountCalculator>();

            // As-is
            container.Register<OrderBuilder>();
            container.Register<ApplicationOrchestrator>();

            // Verify & Return
            container.Verify();
            return container;
        }

        private static IRepository<CustomerType> CreateCustomerTypeRepository()
        {
            var repo = new InMemRepository<CustomerType>();

            repo.Create(new CustomerType
            {
                Id = 1,
                Description = "Private",
                GiveAwayEvery = 0,
                UnitDiscountPercentage = 0
            });
            repo.Create(new CustomerType
            {
                Id = 2,
                Description = "Small Corporate",
                GiveAwayEvery = 0,
                UnitDiscountPercentage = 10
            });
            repo.Create(new CustomerType
            {
                Id = 3,
                Description = "Large Corporate",
                GiveAwayEvery = 3,
                UnitDiscountPercentage = 10
            });
            return repo;

        }

        private static IRepository<Product> CreateProductRepository()
        {
            var repo = new InMemRepository<Product>();
            repo.Create(new Product
            {
                Id = 101,
                Name = "Pencil",
                UnitListPrice = 8
            });
            repo.Create(new Product
            {
                Id = 102,
                Name = "Eraser",
                UnitListPrice = 20
            });
            return repo;

        }

        private static IRepository<Order> CreateOrderRepository()
        {
            var repo = new InMemRepository<Order>();
            return repo;
        }
    }
}
