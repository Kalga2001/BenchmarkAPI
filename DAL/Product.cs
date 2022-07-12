using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BenchmarkAPI.DAL
{
    public  class Product
    {
        public Product()
        {
            ProductsOffers = new HashSet<ProductsOffer>();
        }

        public Guid ProductId { get; set; }
        public string ProductName { get; set; } 
 
        public string CreateBy { get; set; } 

        public DateTime CreateDate { get; set; }
     
        public string UpdateBy { get; set; } 

        public DateTime UpdateDate { get; set; }

        public string? CreatedIp { get; set; }

        public string? UpdatedIp { get; set; }
    
        public bool? IsActive { get; set; }
        
        public bool? IsDeleted { get; set; }

        public virtual ICollection<ProductsOffer> ProductsOffers { get; set; }
    }
}
