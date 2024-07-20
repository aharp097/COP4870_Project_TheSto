using STO.Models;

namespace Sto.API.Database
{
    public static class FauxDatabase
    {
        public static List<Product> Products { get;} =  new List<Product>
            {
                new Product{Id = 1,Name = "Product 1", Price=1.75M, Stock=1}
                , new Product{Id = 2,Name = "Product 2", Price=10M, Stock=10}
                , new Product{Id = 3,Name = "Product 3", Price=137.11M, Stock=100}

            };
        public static int LastID
        {
            get
            {
                if (Products?.Any() ?? false)
                {
                    return Products?.Select(c => c.Id)?.Max() ?? 0;
                }
                return 0;
            }
        }
    }
}
