namespace BenchmarkAPI.Common.ResultDtos.ProductsSizeOptionsDto
{
    public class DeleteProductSizeOptionResultDto
    {
        public DeleteProductSizeOptionResultDto()
        {
            IsDeleted = false;
            Code = 0;
            Status = string.Empty;
        }

        public int Code { get; set; }

        public string Status { get; set; }

        public bool IsDeleted { get; set; }
    }
}
