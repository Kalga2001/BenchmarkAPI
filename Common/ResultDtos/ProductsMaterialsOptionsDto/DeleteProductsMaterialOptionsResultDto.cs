namespace BenchmarkAPI.Common.ResultDtos.ProductsMaterialsOptionsDto
{
    public class DeleteProductsMaterialOptionsResultDto
    {
        public DeleteProductsMaterialOptionsResultDto()
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
