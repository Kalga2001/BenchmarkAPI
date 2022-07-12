using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using BenchmarkAPI.DAL;

namespace BenchmarkAPI.DAL
{
    public class ProductsOffer
    {
        public Guid OfferId { get; set; }
     
        public Guid? MaterialOptionId { get; set; }

        public Guid? SizeOptionId { get; set; }
        public decimal? Price { get; set; }
  
        public Guid? ProductId { get; set; }
        public string CreatedBy { get; set; } 

        public DateTime CreatedDate { get; set; }
       
        public string UpdatedBy { get; set; }
     
        public DateTime UpdatedDate { get; set; }

        public bool? IsActive { get; set; }
  
        public bool? IsDeleted { get; set; }

        public string? CreatedIp { get; set; }

        public string? UpdatedIp { get; set; }

        [JsonIgnore]
        public ProductsMaterialOption? MaterialOption { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        [JsonIgnore]
        public  ProductsSizeOption? SizeOption { get; set; }
    }
}
