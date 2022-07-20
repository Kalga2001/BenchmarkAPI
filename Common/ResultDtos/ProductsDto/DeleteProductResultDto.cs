namespace BenchmarkAPI.Common.ResultDtos.ProductsDto
{
    public class DeleteProductResultDto
    {
            public DeleteProductResultDto()
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
