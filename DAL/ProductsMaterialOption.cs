using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BenchmarkAPI.DAL
{
    public  class ProductsMaterialOption
    {
        public ProductsMaterialOption()
        {
            ProductsOffers = new HashSet<ProductsOffer>();
        }

        public Guid MaterialOptionId { get; set; }
        public string? Quentity { get; set; }
        public Guid? MaterialId { get; set; }

        public Material? Material { get; set; }
        [JsonIgnore]
        public ICollection<ProductsOffer> ProductsOffers { get; set; }
    }
}
