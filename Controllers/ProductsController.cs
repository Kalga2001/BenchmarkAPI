using BenchmarkAPI.Common.ResultDtos.ProductsDto;
using BenchmarkAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Net;

namespace BenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private ILogger<ProductsController> _logger;


        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }



        [HttpGet("GetAll")]
        public async Task<GetProductResultDto> GetProducts()
        {
            var result = new GetProductResultDto();
            try
            {
                using (var _context = new ProductsDbContext())
                {
                    List<Product> allProducts = await _context.Products.Where(p => p.IsDeleted == false && p.IsActive == true).ToListAsync();

                    if (allProducts.Any())
                    {
                        result.Products = allProducts;
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
                _logger.LogError("[{1}]:Error in Get Products {2}.", DateTime.Now, ex.InnerException);
            }
            return result;
        }


        [HttpGet]
        public async Task<GetProductResultDto> GetProductByName(string name)
        {
            var result = new GetProductResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Completed";
                result.Code = 400;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var product = (from p in _context.Products
                                   where p.ProductName == name && p.IsDeleted == false && p.IsActive != false
                                   select p);

                    if (product.Count() > 0)
                    {
                        result.Products = product.ToList();
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
                _logger.LogError("[{1}]:Error in Get Products By Name {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPut]
        public async Task<UpdateProductResultDto> UpdateProductByName(string oldName, string newName)
        {
            var result = new UpdateProductResultDto();

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

                    var product = _context.Products.First(n => n.ProductName == oldName && n.IsDeleted != true);


                    if (product != null)
                    {

                        product.ProductName = newName;
                        product.UpdatedIp = Dns.GetHostName();
                        product.UpdatedDate = DateTime.Now;
                        product.UpdatedBy = Environment.UserName;

                        _context.Entry(product).State = EntityState.Modified;
                        _context.Update(product);


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
                _logger.LogError("[{1}]:Error in Update Product {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPost]
        public async Task<CreateProductResultDto> CreateProductByName(string name)
        {
            var result = new CreateProductResultDto();
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
                    Product product = new Product();

                        product.ProductName = name;
                        product.ProductId = Guid.NewGuid();
                        product.IsActive = true;
                        product.CreatedBy = Environment.UserName;
                        product.CreatedDate = DateTime.Now;
                        product.CreatedIp = Dns.GetHostName();
                        product.IsDeleted = false;
                    

                        result.Status = "Created";
                        result.Code = 201;
                        result.IsCreated = true;
                        _context.Products.Add(product);
                        _context.SaveChanges();
                    
                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsCreated = false;
                _logger.LogError("[{1}]:Error in Create Product {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }



        [HttpDelete]
        public async Task<DeleteProductResultDto> DeleteProductByName(string name)
        {

            var result = new DeleteProductResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Deleted";
                result.Code = 400;
                result.IsDeleted = false;
            }


            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var product = _context.Products.First(n => n.ProductName == name && n.IsActive == true);

                    if (product != null)
                    {
                        product.IsActive = false;
                        product.IsDeleted = true;

                        result.Status = "Deleted";
                        result.Code = 200;
                        result.IsDeleted = true;
                        _context.Entry(product).State = EntityState.Modified;
                        _context.Products.Update(product);
                        _context.SaveChanges();
                    }

                    else
                    {
                        result.Status = "No Deleted";
                        result.Code = 400;
                        result.IsDeleted = false;
                    }

                }

            }


            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsDeleted = false;
                _logger.LogError("[{1}]:Error in Delete Product {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }


    }
}



