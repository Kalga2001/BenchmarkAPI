namespace BenchmarkAPI.Common.ResultDtos.ProductsSizeOptionsDto
{
    public class UpdateProductSizeOptionResultDto
    {
        public UpdateProductSizeOptionResultDto()
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
