namespace BenchmarkAPI.Common.ResultDtos.ProductsOffersDto
{
    public class DeleteOffersResultDto
    {
        public DeleteOffersResultDto()
        {
            IsDeleted = false;
            Code = 0;
            Status = string.Empty;
        }

        public int Code { get; set; }

        public string Status { get; set; }

        public bool IsDeleted{ get; set; }
    }
}
