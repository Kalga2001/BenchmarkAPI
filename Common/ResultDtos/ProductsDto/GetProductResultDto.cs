using System.Collections.Generic;
using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.ResultDtos.ProductsDto
{
    public class GetProductResultDto
    {
        public GetProductResultDto()
        {
            Products = new List<Product>();
            Code = 0;
            Status = string.Empty;
        }

        public List<Product> Products { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
