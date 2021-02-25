using System;
using System.Collections.Generic;
using OrderRegistrar.Domain.Models;

// Would probably use validation here (i like FluentValidation)

namespace OrderRegistrar.Domain.Services
{
    public class OrderBuilder
    {
        private readonly IDiscountCalculator _discountCalculator;
        private Order _currentOrder;
        private readonly Random _orderNrGenerator;

        public List<OrderItem> ShoppingCart => _currentOrder.Items;

        public OrderBuilder(IDiscountCalculator discountCalculator)
        {
            _discountCalculator = discountCalculator;
            _orderNrGenerator = new Random(111);
        }

        public void Start(CustomerType customerType)
        {
            _currentOrder = new Order
            {
                CustomerType = customerType, 
                Number = _orderNrGenerator.Next(99999).ToString(),
                Items = new List<OrderItem>()
            };
        }

        public void AddOrderItem(Product product, int quantity)
        {
            var orderItem = new OrderItem();
            orderItem.OrderedProduct = product;
            orderItem.OrderedQuantity = quantity;
            orderItem.UnitListPrice = product.UnitListPrice;

            _currentOrder.Items.Add(orderItem);
        }

        public Order Finish()
        {
            _discountCalculator.Apply(_currentOrder);
            return _currentOrder;
        }

        public void Cancel()
        {
            _currentOrder = null;
        }
    }
}
