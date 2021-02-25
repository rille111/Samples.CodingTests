using System.Collections.Generic;
using OrderRegistrar.Domain.Models;

namespace OrderRegistrar.Tests.Stubs
{
    public static class ProductStubs
    {
        public static IEnumerable<Product> Create()
        {
            yield return new Product
            {
                Id = 101,
                Name = "Pen",
                UnitListPrice = 8
            };
            yield return new Product
            {
                Id = 105,
                Name = "Eraser",
                UnitListPrice = 10
            };
            yield return new Product
            {
                Id = 201,
                Name = "Stapler",
                UnitListPrice = 22
            };
        }
    }
}
