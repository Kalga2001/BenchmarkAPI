using BenchmarkAPI.Common.ResultDtos.ProductsMaterialsOptionsDto;
using BenchmarkAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace BenchmarkAPI.Controllers
{
    public class ProductsMaterialOptionsController
    {
        private ILogger<ProductsMaterialOptionsController> _logger;
        public ProductsMaterialOptionsController(ILogger<ProductsMaterialOptionsController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllProductsMaterialOptions")]
        public async Task<GetProductsMaterialsOptionsResultDto> GetProductsMaterialOptions()
        {
            var result = new GetProductsMaterialsOptionsResultDto();
            try
            {
                using (var _context = new ProductsDbContext())
                {
                    List<ProductsMaterialOption> allProductsMaterialOptions = await _context.ProductsMaterialOptions
                        .Where(p => p.IsDeleted== false && p.IsActive == true).ToListAsync();

                    if (allProductsMaterialOptions.Any())
                    {
                        result.ProductsMaterialOption = allProductsMaterialOptions;
                        result.Status = "Completed";
                        result.Code = 200;
                    }
                    else
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                _logger.LogError("[{1}]:Error in Get Products Material Option {2}.", DateTime.Now, ex.InnerException);
            }
            return result;
        }


        [HttpGet("GetProductsMaterialOptions")]
        public async Task<GetProductsMaterialsOptionsResultDto> GetProductsMaterialOptionsByProductName(string name)
        {
            var result = new GetProductsMaterialsOptionsResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "Not Found";
                result.Code = 404;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {
                    var product = _context.Products.First(p => p.ProductName == name);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId);

                    if(offer== null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                    }

                   var materialOptions=await (from m in _context.ProductsMaterialOptions
                                        where m.MaterialOptionId==offer.MaterialOptionId && m.IsDeleted == false && m.IsActive != false
                                        select m).ToListAsync();

                    if (materialOptions.Count() > 0)
                    {
                        result.ProductsMaterialOption = materialOptions;
                        result.Status = "Completed";
                        result.Code = 200;
                    }

                    else
                    {
                        result.ProductsMaterialOption = null;
                        result.Status = "Not Found";
                        result.Code = 404;
                    }
                   
                }

            }
            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                _logger.LogError("[{1}]:Error in Get Products Material Options By Quentity {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPut("UpdateProductsMaterialOptions")]
        public async Task<UpdateProductsMaterialOptionsResultDto> UpdateProductsMaterialOptionsByProductName(string name, string newQuentity)
        {
            var result = new UpdateProductsMaterialOptionsResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Updated";
                result.Code = 400;
                result.IsUpdated = false;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var product = _context.Products.First(p => p.ProductName == name);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsUpdated = false;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId);

                    if (offer == null)
                    { 
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsUpdated = false;
                    }

                    var materialOptions = _context.ProductsMaterialOptions.First(m => m.MaterialOptionId == offer.MaterialOptionId);

                    if (materialOptions != null)
                    {
                        materialOptions.Quentity = newQuentity;

                        materialOptions.UpdatedIp = Dns.GetHostName();
                        materialOptions.UpdatedDate = DateTime.Now;
                        materialOptions.UpdatedBy = Environment.UserName;

                        _context.Entry(materialOptions).State = EntityState.Modified;
                        _context.Update(materialOptions);
                        _context.SaveChanges();

                        result.Status = "Updated";
                        result.Code = 204;
                        result.IsUpdated = true;

                    }

                    else
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsUpdated = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsUpdated = false;
                _logger.LogError("[{1}]:Error in Update Products Material Options {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPost("CreateProductsMaterialOptions")]
        public async Task<CreateProductsMaterialOptionsResultDto> CreateProductsMaterialOptionsByProductName(string name, string quentity)
        {
            var result = new CreateProductsMaterialOptionsResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Created";
                result.Code = 400;
                result.IsCreated = false;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {
                    var product = _context.Products.First(p => p.ProductName == name);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsCreated = false;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId);

                    if (offer == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsCreated = false;
                    }

                    var materialOptions = _context.ProductsMaterialOptions.First(m => m.MaterialOptionId == offer.MaterialOptionId);

                    materialOptions.Quentity = quentity;
                    materialOptions.MaterialOptionId = Guid.NewGuid();
                    materialOptions.IsActive = true;
                    materialOptions.CreatedBy = Environment.UserName;
                    materialOptions.CreatedDate = DateTime.Now;
                    materialOptions.CreatedIp = Dns.GetHostName();
                    materialOptions.IsDeleted = false;

                    result.Status = "Created";
                    result.Code = 201;
                    result.IsCreated = true;

                    _context.ProductsMaterialOptions.Add(materialOptions);
                    _context.SaveChanges();
                    
                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsCreated = false;
                _logger.LogError("[{1}]:Error in Create Product Material Option {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }



        [HttpDelete("DeleteProductMaterialOptions")]
        public async Task<DeleteProductsMaterialOptionsResultDto> DeleteProductMaterialOptions(string name)
        {

            var result = new DeleteProductsMaterialOptionsResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Deleted";
                result.Code = 400;
                result.IsDeleted= false;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var product = _context.Products.First(p => p.ProductName == name);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsDeleted = false;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId);

                    if (offer == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsDeleted = false;
                    }

                    var materialOptions = _context.ProductsMaterialOptions.First(m => m.MaterialOptionId == offer.MaterialOptionId);
                     
                    if(materialOptions!=null)
                    {
                        materialOptions.IsActive = false;
                        materialOptions.IsDeleted = true;
                       
                        _context.Entry(materialOptions).State = EntityState.Modified;
                        _context.ProductsMaterialOptions.Update(materialOptions);
                        result.Status = "Deleted";
                        result.Code = 200;
                        result.IsDeleted = true;
                        _context.SaveChanges();
                    }

                    else
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsDeleted = false;
                    }


                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsDeleted = false;
                _logger.LogError("[{1}]:Error in Delete Product Material Option {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }

    }
}
