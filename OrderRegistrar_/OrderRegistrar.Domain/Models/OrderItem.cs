namespace OrderRegistrar.Domain.Models
{
    public class OrderItem
    {
        public int ProductId => OrderedProduct.Id;
        public Product OrderedProduct { get; set; }
        public int OrderedQuantity { get; set; }
        public int QuantityGivenAway { get; set; }
        public decimal UnitCustomerPrice { get; set; }
        /// <summary>
        /// At time of order (good for book-keeping ;) )
        /// </summary>
        public decimal UnitListPrice { get; set; }
        public decimal UnitAppliedDiscount { get; set; }

        public decimal SumDiscountSavings { get; set; }
        public decimal SumGiveAwaySavings { get; set; }
        public decimal SumCustomerPrice { get; set; }
        public decimal SumOfAllSavings { get; set; }
    }
}