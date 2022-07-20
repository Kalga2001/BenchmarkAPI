using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.ResultDtos.ProductsSizeOptionsDto
{
    public class GetProductSizeOptionResultDto
    {
        public GetProductSizeOptionResultDto()
        {
            ProductsSizeOption = new List<ProductsSizeOption>();
            Code = 0;
            Status = string.Empty;
        }

        public List<ProductsSizeOption> ProductsSizeOption { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
