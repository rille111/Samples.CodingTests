using System.Collections.Generic;
using OrderRegistrar.Domain.Concepts;

namespace OrderRegistrar.Domain.Models
{
    public class Order : IAggregateRoot
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public CustomerType CustomerType { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal TotalSavings { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
