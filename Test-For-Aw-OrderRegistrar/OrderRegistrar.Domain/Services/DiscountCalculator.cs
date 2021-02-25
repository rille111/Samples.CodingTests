using System.Linq;
using OrderRegistrar.Domain.Models;

namespace OrderRegistrar.Domain.Services
{
    public interface IDiscountCalculator
    {
        void Apply(Order order);
    }

    public class DiscountCalculator : IDiscountCalculator
    {
        public void Apply(Order order)
        {
            foreach (var orderItem in order.Items)
            {
                CalculateGiveAwaysOnItem(orderItem, order.CustomerType);
                CalculateDiscountOnItem(orderItem, order.CustomerType);
                CalculateSumPriceOnItem(orderItem);
                CalculateSavingsOnItem(orderItem);
            }

            order.TotalPrice = order.Items.Sum(p => p.SumCustomerPrice);
            order.TotalSavings = order.Items.Sum(p => p.SumOfAllSavings);
        }

        private void CalculateSumPriceOnItem(OrderItem orderItem)
        {
            var payForQuantity = orderItem.OrderedQuantity - orderItem.QuantityGivenAway;
            orderItem.SumCustomerPrice = payForQuantity * orderItem.UnitCustomerPrice;
        }

        private void CalculateSavingsOnItem(OrderItem orderItem)
        {
            orderItem.SumGiveAwaySavings = orderItem.QuantityGivenAway * orderItem.UnitListPrice;
            orderItem.SumDiscountSavings = (orderItem.OrderedQuantity - orderItem.QuantityGivenAway) * orderItem.UnitAppliedDiscount;
            orderItem.SumOfAllSavings = orderItem.SumGiveAwaySavings + orderItem.SumDiscountSavings;
        }

        private static void CalculateDiscountOnItem(OrderItem orderItem, CustomerType customerType)
        {
            if (customerType.UnitDiscountPercentage > 0)
            {
                orderItem.UnitAppliedDiscount = orderItem.UnitListPrice * (customerType.UnitDiscountPercentage / 100m);
            }

            orderItem.UnitCustomerPrice = orderItem.UnitListPrice - orderItem.UnitAppliedDiscount;
        }

        private static void CalculateGiveAwaysOnItem(OrderItem orderItem, CustomerType customerType)
        {
            if (customerType.GiveAwayEvery > 0)
            {
                var giveAwayQty = orderItem.OrderedQuantity / customerType.GiveAwayEvery;
                orderItem.QuantityGivenAway = giveAwayQty;
            }
        }
    }
}
