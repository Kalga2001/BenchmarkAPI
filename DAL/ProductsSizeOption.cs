﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BenchmarkAPI.DAL
{
    public  class ProductsSizeOption
    {
        public ProductsSizeOption()
        {
            ProductsOffers = new HashSet<ProductsOffer>();
        }
        [JsonIgnore]
        public Guid SizeOptionId { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        [JsonIgnore]
        public Guid? UnitId { get; set; }
        [JsonIgnore]
        public  Unit? Unit { get; set; }
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
        public  ICollection<ProductsOffer> ProductsOffers { get; set; }
    }
}
