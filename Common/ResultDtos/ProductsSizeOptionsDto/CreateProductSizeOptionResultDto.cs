namespace BenchmarkAPI.Common.ResultDtos.ProductsSizeOptionsDto
{
    public class CreateProductSizeOptionResultDto
    {
        public CreateProductSizeOptionResultDto()
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
