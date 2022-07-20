using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
namespace BenchmarkAPI.DAL
{
    public  class ProductsMaterialOption
    {
        public ProductsMaterialOption()
        {
            ProductsOffers = new HashSet<ProductsOffer>();
        }
        [JsonIgnore]
        public Guid MaterialOptionId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [Required(ErrorMessage = "Please Enter Quentity")]
        public string? Quentity { get; set; }
        [JsonIgnore]
        public Guid? MaterialId { get; set; }
        [JsonIgnore]
        public Material? Material { get; set; }
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
        public ICollection<ProductsOffer> ProductsOffers { get; set; }
    }
}
