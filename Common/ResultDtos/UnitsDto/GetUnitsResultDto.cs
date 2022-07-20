using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.ResultDtos.UnitsDto
{
    public class GetUnitsResultDto
    {
        public GetUnitsResultDto()
        {
            Units = new List<Unit>();
            Code = 0;
            Status = string.Empty;
        }

        public List<Unit> Units { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
