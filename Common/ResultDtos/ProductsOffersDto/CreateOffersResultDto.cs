namespace BenchmarkAPI.Common.ResultDtos.ProductsOffersDto
{
    public class CreateOffersResultDto
    {
        public CreateOffersResultDto()
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
