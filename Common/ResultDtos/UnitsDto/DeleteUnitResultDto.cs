namespace BenchmarkAPI.Common.ResultDtos.UnitsDto
{
    public class DeleteUnitResultDto
    {
        public DeleteUnitResultDto()
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
