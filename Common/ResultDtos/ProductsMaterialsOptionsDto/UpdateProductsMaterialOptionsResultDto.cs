namespace BenchmarkAPI.Common.ResultDtos.ProductsMaterialsOptionsDto
{
    public class UpdateProductsMaterialOptionsResultDto
    {
        public UpdateProductsMaterialOptionsResultDto()
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
