namespace BenchmarkAPI.Common.ResultDtos.UnitsDto
{
    public class UpdateUnitResultDto
    {
        public UpdateUnitResultDto()
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