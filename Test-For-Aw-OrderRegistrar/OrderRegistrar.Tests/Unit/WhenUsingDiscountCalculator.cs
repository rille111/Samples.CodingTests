using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderRegistrar.Domain.Models;
using OrderRegistrar.Domain.Services;
using OrderRegistrar.Tests.Stubs;

// I'd probably use test cases here and there but dunno how MsTest does it (couldn't get nUnit to work, didnt wanna waste time)

namespace OrderRegistrar.Tests.Unit
{
    [TestClass]
    public class WhenUsingDiscountCalculator
    {
        [TestMethod]
        public void ThenGiveAwaysShouldCalculateCorrectly()
        {
            // Arrange
            var orderedProduct = ProductStubs.Create().First();
            var giveAwayEvery = 3;
            var orderedQty = 8;
            var expectedGiveAway = 2;
            var customerType = CreateCustomerType(giveAwayEvery, 0);
            var orderItem = CreateOrderItem(orderedProduct, orderedQty);
            var order = new Order() {CustomerType = customerType, Items = new List<OrderItem> {orderItem}};
            var discCalc = new DiscountCalculator();

            // Act
            discCalc.Apply(order);

            // Assert
            orderItem.QuantityGivenAway
                .Should().Be(expectedGiveAway, $"we ordered {orderedQty} and expected  {expectedGiveAway} give-aways!");
        }

        [TestMethod]
        public void ThenDiscountOnOneOrderItemShouldBeApplied()
        {
            // Arrange
            var orderedProduct = ProductStubs.Create().First();
            orderedProduct.UnitListPrice = 22m;
            var expectedCustomerPrice = 19.8m;
            var expectedDiscount = 2.2m;
            var orderedQty = 1;
            var customerDiscount = 10;
            var customerType = CreateCustomerType(0, customerDiscount);
            var orderItem = CreateOrderItem(orderedProduct, orderedQty);
            var order = new Order { CustomerType = customerType, Items = new List<OrderItem> { orderItem } };
            var discCalc = new DiscountCalculator();

            // Act
            discCalc.Apply(order);

            // Assert
            orderItem.UnitCustomerPrice.Should()
                .Be(expectedCustomerPrice, "discount should have been applied correctly!");
            orderItem.UnitAppliedDiscount.Should()
                .Be(expectedDiscount, "discount should have been applied correctly!");
        }

        [TestMethod]
        public void ThenDiscountOnEntireOrderShouldBeApplied()
        {
            // Arrange
            var products = ProductStubs.Create().ToList();
            var customerDiscount = 20; // original price: 970 without any discount
            var customerType = CreateCustomerType(0, customerDiscount);
            var order = new Order { CustomerType = customerType };
            products[0].UnitListPrice = 22m;
            products[1].UnitListPrice = 30m;
            products[2].UnitListPrice = 45m;
            order.Items.AddRange(new[]
            {
                CreateOrderItem(products[0], 10),
                CreateOrderItem(products[1], 10),
                CreateOrderItem(products[2], 10)
            });
            var discCalc = new DiscountCalculator();

            // Act
            discCalc.Apply(order);

            // Assert
            order.Items[0].UnitListPrice.Should().Be(22m);
            order.Items[0].UnitAppliedDiscount.Should().Be(4.4m);
            order.Items[0].UnitCustomerPrice.Should().Be(17.6m);
            order.Items[0].SumCustomerPrice.Should().Be(176.0m);
            order.Items[0].SumDiscountSavings.Should().Be(44.0m);

            order.TotalSavings.Should().Be(194.0m);
            order.TotalPrice.Should().Be(776.0M); 
        }

        [TestMethod]
        public void ThenDiscountOnEntireOrderWithGiveAwaysShouldBeApplied()
        {
            // Arrange
            var products = ProductStubs.Create().ToList();
            var customerDiscount = 10; 
            var customerType = CreateCustomerType(3, customerDiscount); // Every third unit will be given away
            var order = new Order { CustomerType = customerType };
            products[0].UnitListPrice = 20m;
            products[1].UnitListPrice = 40m;
            order.Items.AddRange(new[]
            {
                CreateOrderItem(products[0], 6), // 2 given away
                CreateOrderItem(products[1], 10), // 3 given away
            });
            var expectedTotalSavingProduct1 = (8m + 40m); // total discount for 4 units and 2 giveaways (listprice used)
            var expectedTotalSavingProduct2 = (28m + 120m); // total discount for 7 units and 3 giveaways (listprice used)

            var discCalc = new DiscountCalculator();

            // Act
            discCalc.Apply(order);

            // Assert
            order.Items[0].SumOfAllSavings.Should().Be(expectedTotalSavingProduct1);
            order.Items[1].SumOfAllSavings.Should().Be(expectedTotalSavingProduct2);
            order.TotalSavings.Should().Be(expectedTotalSavingProduct1 + expectedTotalSavingProduct2);
        }

        // Helpers

        private static OrderItem CreateOrderItem(Product orderedProduct, int orderedQty)
        {
            var orderItem = new OrderItem
            {
                OrderedProduct = orderedProduct,
                OrderedQuantity = orderedQty,
                UnitListPrice = orderedProduct.UnitListPrice
            };
            return orderItem;
        }

        private static CustomerType CreateCustomerType(int giveAwayEvery, int discountPercentage)
        {
            return new CustomerType
            {
                Description = "Some name",
                GiveAwayEvery = giveAwayEvery,
                UnitDiscountPercentage = discountPercentage
            };
        }
    }
}
