namespace BenchmarkAPI.Common.ResultDtos.MaterialsDto
{
    public class CreateMaterialResultDto
    {
        public CreateMaterialResultDto()
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
