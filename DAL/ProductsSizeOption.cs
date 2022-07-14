using System;
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

        public Guid SizeOptionId { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public Guid? UnitId { get; set; }

        public  Unit? Unit { get; set; }
        [JsonIgnore]
        public  ICollection<ProductsOffer> ProductsOffers { get; set; }
    }
}
