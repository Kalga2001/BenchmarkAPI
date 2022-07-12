using System;
using System.Collections.Generic;

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
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; } = null!;
        public DateTime UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public string? CreatedIp { get; set; }
        public string? UpdatedIp { get; set; }

        public  ICollection<ProductsSizeOption> ProductsSizeOptions { get; set; }
    }
}
