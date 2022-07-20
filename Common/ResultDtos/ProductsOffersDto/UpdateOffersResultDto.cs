namespace BenchmarkAPI.Common.ResultDtos.ProductsOffersDto
{
    public class UpdateOffersResultDto
    {
        public UpdateOffersResultDto()
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
