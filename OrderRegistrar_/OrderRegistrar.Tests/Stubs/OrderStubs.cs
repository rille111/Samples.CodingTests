using System.Collections.Generic;
using System.Linq;
using OrderRegistrar.Domain.Models;

namespace OrderRegistrar.Tests.Stubs
{
    public static class OrderStubs
    {
        public static IEnumerable<Order> CreateWithoutDiscounts()
        {
            var custTypes = CustomerTypeStubs.Create().ToArray();
            var products = ProductStubs.Create().ToArray();

            yield return new Order
            {
                Id = 980,
                Number = "1001",
                CustomerType = custTypes[0],
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        OrderedProduct = products[0],
                        OrderedQuantity = 10,
                        QuantityGivenAway = 0,
                        UnitAppliedDiscount = 0,
                        UnitListPrice = products[0].UnitListPrice
                    }
                }
            };
            yield return new Order
            {
                Id = 981,
                Number = "1022",
                CustomerType = custTypes[1],
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        OrderedProduct = products[1],
                        OrderedQuantity = 8,
                        QuantityGivenAway = 0,
                        UnitAppliedDiscount = 0,
                        UnitListPrice = products[1].UnitListPrice
                    }
                }
            };
            yield return new Order
            {
                Id = 982,
                Number = "1033",
                CustomerType = custTypes[2],
                Items = new List<OrderItem>
                {
                    new OrderItem
                    {
                        OrderedProduct = products[2],
                        OrderedQuantity = 8,
                        QuantityGivenAway = 0,
                        UnitAppliedDiscount = 0,
                        UnitListPrice = products[2].UnitListPrice
                    }
                }
            };

        }
    }
}
