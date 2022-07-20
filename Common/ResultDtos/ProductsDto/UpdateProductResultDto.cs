namespace BenchmarkAPI.Common.ResultDtos.ProductsDto
{
    public class UpdateProductResultDto
    {
        public UpdateProductResultDto()
        {
            IsUpdated = false;
            Code = 0;
            Status = string.Empty;
        }

        public int Code { get; set; }

        public string Status { get; set; }

        public bool IsUpdated { get; set; }
    }
}
