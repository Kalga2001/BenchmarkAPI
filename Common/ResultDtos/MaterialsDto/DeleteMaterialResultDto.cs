namespace BenchmarkAPI.Common.ResultDtos.MaterialsDto
{
    public class DeleteMaterialResultDto
    {
        public DeleteMaterialResultDto()
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
