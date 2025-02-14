﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BenchmarkAPI.DAL;

public  class Product
{
    public Product()
    {
        ProductsOffers = new HashSet<ProductsOffer>();
    }

    [JsonIgnore]
    public Guid ProductId { get; set; }

    [StringLength(50, MinimumLength = 3)]

    [Required(ErrorMessage = "Please Enter Product Name")]
    public string ProductName { get; set; }
    [JsonIgnore]
    public string CreatedBy { get; set; } = Environment.UserName;
    [JsonIgnore]
    public DateTime CreatedDate { get; set; }= DateTime.Now;
    [JsonIgnore]
    public string UpdatedBy { get; set; } = Environment.UserName;
    [JsonIgnore]
    public DateTime UpdatedDate { get; set; }= DateTime.Now;
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
