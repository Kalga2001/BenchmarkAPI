using BenchmarkAPI.Common.ResultDtos.ProductsSizeOptionsDto;
using BenchmarkAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace BenchmarkAPI.Controllers
{
    public class ProductsSizeOptionController
    {
        private ILogger<ProductsSizeOptionController> _logger;
        public ProductsSizeOptionController(ILogger<ProductsSizeOptionController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllProductsSizeOptions")]
        public async Task<GetProductSizeOptionResultDto> GetProductsSizeOptions()
        {
            var result = new GetProductSizeOptionResultDto();
            try
            {
                using (var _context = new ProductsDbContext())
                {
                    List<ProductsSizeOption> allProductsSizeOptions = await _context.ProductsSizeOptions
                        .Where(p => p.IsDeleted == false && p.IsActive == true).ToListAsync();

                    if (allProductsSizeOptions.Any())
                    {
                        result.ProductsSizeOption = allProductsSizeOptions;
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
                _logger.LogError("[{1}]:Error in Get Products Size Options {2}.", DateTime.Now, ex.InnerException);
            }
            return result;
        }


        [HttpGet("GetProductsSizeOptionsByProductName")]
        public async Task<GetProductSizeOptionResultDto> GetProductsSizeOptionsByProductName(string name)
        {
            var result = new GetProductSizeOptionResultDto();
            
            if (!ModelState.IsValid)
            {
                result.Status = "No Completed";
                result.Code = 400;
            }

                try
            {
                using (var _context = new ProductsDbContext())
                {
                        var product = _context.Products.First(p => p.ProductName == name && p.IsActive == true && p.IsDeleted == false);
                        if (product == null)
                        {
                            result.Status = "Not Found";
                            result.Code = 404;
                        }

                        var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId && p.IsActive == true && p.IsDeleted == false);

                        if (offer == null)
                        {
                            result.Status = "Not Found";
                            result.Code = 404;
                        }

                        var productOptions = (from p in _context.ProductsSizeOptions
                                              where p.SizeOptionId == offer.SizeOptionId && p.IsDeleted == false && p.IsActive != false
                                              select p);

                        if (productOptions.Count() > 0)
                        {
                            result.ProductsSizeOption = productOptions.ToList();
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
                _logger.LogError("[{1}]:Error in Get Products Material Options By Quentity {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPut("UpdateProductSizeOptionByProductName")]
        public async Task<UpdateProductSizeOptionResultDto> UpdateProductSizeOptionByProductName
            (string name, double newHeight,double newWidth)
        {
            var result = new UpdateProductSizeOptionResultDto();

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

                    var product = _context.Products.First(p => p.ProductName == name && p.IsActive == true && p.IsDeleted == false);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsUpdated = false;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId && p.IsActive == true && p.IsDeleted == false);

                    if (offer == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsUpdated = false;
                    }

                    var productOptions = _context.ProductsSizeOptions.First(p => p.SizeOptionId == offer.SizeOptionId && p.IsActive == true && p.IsDeleted == false);

                    if (productOptions != null)
                    {


                        productOptions.Height = newHeight;
                        productOptions.Width = newWidth;
                        productOptions.UpdatedIp = Dns.GetHostName();
                        productOptions.UpdatedDate = DateTime.Now;
                        productOptions.UpdatedBy = Environment.UserName;


                        _context.Entry(productOptions).State = EntityState.Modified;
                        _context.Update(productOptions);

                        result.Status = "Updated";
                        result.Code = 204;
                        result.IsUpdated = true;
                        _context.SaveChanges();



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



        [HttpPost("CreateProductSizeOption")]
        public async Task<CreateProductSizeOptionResultDto> CreateProductSizeOptionByProductName(string name, double height, double width)
        {
            var result = new CreateProductSizeOptionResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Updated";
                result.Code = 400;
                result.IsCreated = false;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var product = _context.Products.First(p => p.ProductName == name && p.IsActive == true && p.IsDeleted == false);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsCreated = false;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId && p.IsActive == true && p.IsDeleted == false);

                    if (offer == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsCreated = false;
                    }

                    var productOptions = _context.ProductsSizeOptions.First(p => p.SizeOptionId == offer.SizeOptionId && p.IsActive == true && p.IsDeleted == false);

                    if (productOptions == null)
                    {
                        productOptions.Height = height;
                        productOptions.Width = width;
                        productOptions.SizeOptionId = Guid.NewGuid();
                        productOptions.IsActive = true;
                        productOptions.CreatedBy = Environment.UserName;
                        productOptions.CreatedDate = DateTime.Now;
                        productOptions.CreatedIp = Dns.GetHostName();
                        productOptions.IsDeleted = false;


                        _context.ProductsSizeOptions.Add(productOptions);
                        _context.SaveChanges();

                    }

                    else
                    {
                        result.Status = "No Created";
                        result.Code = 400;
                        result.IsCreated = false;
                    }

                   
                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                _logger.LogError("[{1}]:Error in Create Product Material Option {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }



        [HttpDelete("DeleteProductSizeOption")]
        public async Task<DeleteProductSizeOptionResultDto> DeleteProductSizeOption(string name, double height, double width)
        {

            var result = new DeleteProductSizeOptionResultDto();

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

                    var product = _context.Products.First(p => p.ProductName == name && p.IsActive == true && p.IsDeleted == false);

                    if (product == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsDeleted = false;
                    }

                    var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId && p.IsActive == true && p.IsDeleted == false);

                    if (offer == null)
                    {
                        result.Status = "Not Found";
                        result.Code = 404;
                        result.IsDeleted= false;
                    }

                    var productOptions = _context.ProductsSizeOptions.First(p => p.SizeOptionId == offer.SizeOptionId && p.IsActive == true && p.IsDeleted == false);

                    if(productOptions!=null)
                    {     
                        product.IsActive = false;
                        product.IsDeleted = true;
                        result.Status = "Deleted";
                        result.Code = 200;
                        result.IsDeleted = true;

                        _context.Entry(product).State = EntityState.Modified;
                        _context.ProductsSizeOptions.Update(product);
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
                _logger.LogError("[{1}]:Error in Delete Product Size Option {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }

    }
}
