using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.ResultDtos.ProductsOffersDto
{
    public class GetOffersResultDto
    {
        public GetOffersResultDto()
        {
            ProductsOffers = new List<ProductsOffer>();
            Code = 0;
            Status = string.Empty;
        }

        public List<ProductsOffer> ProductsOffers { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
