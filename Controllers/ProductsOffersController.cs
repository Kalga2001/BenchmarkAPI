using BenchmarkAPI.Common.ResultDtos.ProductsOffersDto;
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
    public class ProductsOffersController : ControllerBase
    {

        private ILogger<ProductsOffersController> _logger;


        public ProductsOffersController(ILogger<ProductsOffersController> logger)
        {
            _logger = logger;

        }


        [HttpGet("GetAll")]
        public async Task<GetOffersResultDto> GetProductsOffers()
        {
            var result = new GetOffersResultDto();
            try
            {
                using (var _context = new ProductsDbContext())
                {
                    List<ProductsOffer> allOffers = await _context.ProductsOffers.Where(p => p.IsDeleted == false && p.IsActive == true).ToListAsync();

                    if (allOffers.Any())
                    {
                        result.ProductsOffers = allOffers;
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
                _logger.LogError("[{1}]:Error in Get Products Offers {2}.", DateTime.Now, ex.InnerException);
            }
            return result;
        }


        [HttpGet]
        public async Task<GetOffersResultDto> GetProductsOffersByProductName(string name)
        {
            var result = new GetOffersResultDto();

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var product = _context.Products.First(p => p.ProductName == name);

                    List<ProductsOffer> offers = await _context.ProductsOffers
                    .Where(p => p.IsDeleted == false && p.IsActive == true && p.ProductId == product.ProductId)
                    .ToListAsync();

                    if (offers != null)
                    {
                        result.ProductsOffers = offers;
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
                _logger.LogError("[{1}]:Error in Get Products Offer By Price {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPut]
        public async Task<UpdateOffersResultDto> UpdateProductsOffers(string name, decimal newPrice)
        {
            var result = new UpdateOffersResultDto();
           
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

                    if (offer != null)
                    {

                        offer.Price = newPrice;
                        offer.UpdatedIp = Dns.GetHostName();
                        offer.UpdatedDate = DateTime.Now;
                        offer.UpdatedBy = Environment.UserName;


                        _context.Entry(offer).State = EntityState.Modified;
                        _context.Update(offer);
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
                _logger.LogError("[{1}]:Error in Update Price {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPost]
        public async Task<CreateOffersResultDto> CreateProductsOfferByProductName(string name, decimal newPrice)
        {
            var result = new CreateOffersResultDto();
            if (!ModelState.IsValid)
            {
                result.Status = "No Created";
                result.Code = 400;
                result.IsCreated= false;
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

                        else
                        {
                            result.Status = "No Created";
                            result.Code = 400;
                            result.IsCreated = false;
                        }


                        var offer = _context.ProductsOffers.First(p => p.ProductId == product.ProductId);


                        if (offer == null)
                        {
                            offer.Price = newPrice;
                            offer.OfferId = Guid.NewGuid();
                            offer.IsActive = true;
                            offer.CreatedBy = Environment.UserName;
                            offer.CreatedDate = DateTime.Now;
                            offer.CreatedIp = Dns.GetHostName();
                            offer.IsDeleted = false;

                            result.Code = 201;
                            result.Status = "Created";
                            result.IsCreated = true;
                            _context.ProductsOffers.Add(offer);
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
                result.IsCreated = false;
                _logger.LogError("[{1}]:Error in Create Product Offer {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }



        [HttpDelete]
        public async Task<DeleteOffersResultDto> DeleteProductOffers(string name)
        {

            var result = new DeleteOffersResultDto();

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


                    else
                    {
                        offer.IsActive = false;
                        offer.IsDeleted = true;
                        result.Status = "Deleted";
                        result.Code = 200;
                        result.IsDeleted = true;
                        _context.Entry(offer).State = EntityState.Modified;
                        _context.ProductsOffers.Update(offer);
                        _context.SaveChanges();
                    }

                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsDeleted = false;
                _logger.LogError("[{1}]:Error in Delete Product Offer {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }
    }
}
