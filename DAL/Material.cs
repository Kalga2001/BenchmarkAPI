using System;
using System.Collections.Generic;

namespace BenchmarkAPI.DAL
{
    public  class Material
    {
        public Material()
        {
            ProductsMaterialOptions = new HashSet<ProductsMaterialOption>();
        }

        public Guid MaterialId { get; set; }
        public string MaterialName { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedIp { get; set; }
        public string? UpdatedIp { get; set; }

        public  ICollection<ProductsMaterialOption> ProductsMaterialOptions { get; set; }
    }
}
