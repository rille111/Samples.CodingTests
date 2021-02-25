using OrderRegistrar.Domain.Concepts;

namespace OrderRegistrar.Domain.Models
{
    public class CustomerType : IAggregateRoot
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int UnitDiscountPercentage { get; set; }
        public int GiveAwayEvery { get; set; }
    }
}