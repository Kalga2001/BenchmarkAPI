namespace BenchmarkAPI.Common.ResultDtos.ProductsDto
{
    public class CreateProductResultDto
    {
        public CreateProductResultDto()
        {
            IsCreated = false;
            Code = 0;
            Status = string.Empty;
        }

        public int Code { get; set; }

        public string Status { get; set; }

        public bool IsCreated { get; set; }
    }
}
