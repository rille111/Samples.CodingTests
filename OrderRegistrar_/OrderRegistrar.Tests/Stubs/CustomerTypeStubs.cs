using System.Collections.Generic;
using OrderRegistrar.Domain.Models;

namespace OrderRegistrar.Tests.Stubs
{
    public static class CustomerTypeStubs
    {
        public static IEnumerable<CustomerType> Create()
        {
            yield return new CustomerType
            {
                Id = 10,
                Description = "Small",
                GiveAwayEvery = 0,
                UnitDiscountPercentage = 0
            };
            yield return new CustomerType
            {
                Id = 20,
                Description = "Medium",
                GiveAwayEvery = 0,
                UnitDiscountPercentage = 10
            };
            yield return new CustomerType
            {
                Id = 20,
                Description = "Large",
                GiveAwayEvery = 3,
                UnitDiscountPercentage = 10
            };
        }
    }
}
