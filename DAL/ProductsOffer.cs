using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using BenchmarkAPI.DAL;
using System.ComponentModel.DataAnnotations;

namespace BenchmarkAPI.DAL
{
    public class ProductsOffer
    {
        [JsonIgnore]
        public Guid OfferId { get; set; }
        [JsonIgnore]
        public Guid? MaterialOptionId { get; set; }
        [JsonIgnore]
        public Guid? SizeOptionId { get; set; }

        [Range(0.1,double.MaxValue,ErrorMessage="Price must be greater than zero")]
        public decimal? Price { get; set; }
        [JsonIgnore]
        public Guid? ProductId { get; set; }
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
        public ProductsMaterialOption? MaterialOption { get; set; }
        [JsonIgnore]
        public Product? Product { get; set; }

        [JsonIgnore]
        public  ProductsSizeOption? SizeOption { get; set; }
    }
}
