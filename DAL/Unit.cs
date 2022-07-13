using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BenchmarkAPI.DAL
{
    public  class Unit
    {
        public Unit()
        {
            ProductsSizeOptions = new HashSet<ProductsSizeOption>();
        }

        public Guid UnitId { get; set; }
        public string UnitName { get; set; } = null!;
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


        public ICollection<ProductsSizeOption> ProductsSizeOptions { get; set; }
    }
}
