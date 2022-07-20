using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace BenchmarkAPI.DAL
{
    public  class Material
    {
        public Material()
        {
            ProductsMaterialOptions = new HashSet<ProductsMaterialOption>();
        }

        [JsonIgnore]
        public Guid MaterialId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Please Enter Material Name")]
        public string MaterialName { get; set; } = null!;
        [JsonIgnore]
        public string CreatedBy { get; set; } = Environment.UserName;
        [JsonIgnore]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public string UpdatedBy { get; set; } = Environment.UserName;
        [JsonIgnore]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        [JsonIgnore]
        public string? CreatedIp { get; set; }
        [JsonIgnore]
        public string? UpdatedIp { get; set; }
        [JsonIgnore]
        public bool? IsActive { get; set; }
        [JsonIgnore]
        public bool? IsDeleted { get; set; }

        [JsonIgnore]
        public ICollection<ProductsMaterialOption> ProductsMaterialOptions { get; set; }
    }
}
