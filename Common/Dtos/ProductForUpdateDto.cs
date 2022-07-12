using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Common.Dtos
{
    public class ProductForUpdateDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CreateBy { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateDate { get; set; }
        public string? CreatedIp { get; set; }
        public string? UpdatedIp { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ProductsOffer> ProductsOffers { get; set; }
    }
}
