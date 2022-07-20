using BenchmarkAPI.DAL;
using System.Collections.Generic;

namespace BenchmarkAPI.Common.ResultDtos.MaterialsDto
{
    public class GetMaterialResultDto
    {
        public GetMaterialResultDto()
        {
            Materials = new List<Material>();
            Code = 0;
            Status = string.Empty;
        }

        public List<Material> Materials { get; set; }
        public int Code { get; set; }

        public string Status { get; set; }
    }
}
