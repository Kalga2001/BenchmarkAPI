namespace BenchmarkAPI.Common.ResultDtos.UnitsDto
{
    public class CreateUnitResultDto
    {
        public CreateUnitResultDto()
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
