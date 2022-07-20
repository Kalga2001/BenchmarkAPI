namespace BenchmarkAPI.Common.ResultDtos.MaterialsDto
{
    public class UpdateMaterialResultDto
    {
        public UpdateMaterialResultDto()
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