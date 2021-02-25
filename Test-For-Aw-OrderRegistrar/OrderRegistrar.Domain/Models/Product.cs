using OrderRegistrar.Domain.Concepts;

namespace OrderRegistrar.Domain.Models
{
    public class Product : IAggregateRoot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitListPrice { get; set; }
    }
}