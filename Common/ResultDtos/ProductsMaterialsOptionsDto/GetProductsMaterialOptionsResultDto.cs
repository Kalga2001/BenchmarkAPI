using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.ResultDtos.ProductsMaterialsOptionsDto
{
    public class GetProductsMaterialsOptionsResultDto
    {

        public GetProductsMaterialsOptionsResultDto()
        {
            ProductsMaterialOption = new List<ProductsMaterialOption>();
            Code = 0;
            Status = string.Empty;
        }

        public List<ProductsMaterialOption> ProductsMaterialOption { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }

}
