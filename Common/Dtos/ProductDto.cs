using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.Dtos
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } 
        public List<ProductsOffer> ProductsOffers { get; set; }
    }
}
