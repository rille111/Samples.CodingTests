using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OrderRegistrar.Domain.Models;
using OrderRegistrar.Domain.Services;
using OrderRegistrar.Tests.Stubs;

namespace OrderRegistrar.Tests.Unit
{
    [TestClass]
    public class WhenCreatingOrdersWithOrderBuilder
    {
        [TestMethod]
        public void ThenDiscountsShouldBeCalculated()
        {
            // Arrange
            var discountCalc = new Mock<IDiscountCalculator>();
            discountCalc.Setup(p => p.Apply(It.IsAny<Order>()));
            var orderBuilder = new OrderBuilder(discountCalc.Object);

            // Act
            orderBuilder.Start(CustomerTypeStubs.Create().First());
            orderBuilder.AddOrderItem(ProductStubs.Create().First(), 13);
            var order = orderBuilder.Finish();

            // Assert
            order.Items.Count.Should().Be(1);
            discountCalc.VerifyAll();
        }
    }
}
