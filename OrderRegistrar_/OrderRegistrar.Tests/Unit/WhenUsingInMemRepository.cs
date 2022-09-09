using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderRegistrar.Domain.Models;
using OrderRegistrar.Infrastructure.Repository;
using OrderRegistrar.Tests.Stubs;

namespace OrderRegistrar.Tests.Unit
{
    [TestClass]
    public class WhenUsingInMemRepository
    {
        [TestMethod]
        public void ThenCreateSomeOrdersAndPersistThem()
        {
            // Arrange
            var repo = new InMemRepository<Order>();
            var orders = OrderStubs.CreateWithoutDiscounts().ToList();
            var expectedOrderCount = orders.Count;

            // Act
            orders.ForEach(repo.Create);

            // Assert
            repo.ItemsWhere(p => true).Count()
                .Should().Be(expectedOrderCount, $"there should be just as many orders in the repo!");
        }
    }
}
