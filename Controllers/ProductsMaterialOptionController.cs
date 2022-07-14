using BenchmarkAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsMaterialOptionController : ControllerBase
    {
        private readonly ProductsDbContext _context;
        private ILogger<ProductsMaterialOptionController> _logger;


        public ProductsMaterialOptionController(ILogger<ProductsMaterialOptionController> logger, ProductsDbContext context)
        {
            _logger = logger;
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsMaterialOption>>> GetProductsMaterialOptions()
        {
            var result = _context.ProductsMaterialOptions;


            return await result.ToListAsync();

        }

        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<ProductsMaterialOption>>> GetOfferById(string name)
        {

            Product product = new Product();
            if (product.ProductName != name)
            {
                _logger.LogInformation($"Not found product offer with name {name}");
            }


            var result = (from p in _context.Products
                          join o in _context.ProductsOffers
                           on p.ProductId equals o.ProductId
                          where p.ProductName == name && o.IsDeleted == false && o.IsActive != false
                          select o).ToListAsync();


            try
            {
                var res = OffersExists(name);

                if (res != true)
                {
                    _logger.LogError($"Offer at ProductName: {name}, hasn't been found in db.");
                }
                else
                {
                    _logger.LogInformation($"Returned offer at ProductName: {name}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return await result;
        }


        [HttpPut("{id}")]
        public IActionResult UpdateProductOffers([FromBody] ProductsOffer offer, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Entry param is empty ");
            }

            ProductsOffer offer1 = new ProductsOffer();

            try
            {
                if (offer == null)
                {
                    return StatusCode(404, "Offers not found");
                }

                offer1.Price = offer.Price;
                offer1.OfferId = offer.OfferId;
                offer1.IsActive = true;
                offer1.CreatedBy = Environment.UserName;
                offer1.CreatedDate = DateTime.Now;
                offer1.CreatedIp = offer.CreatedIp;
                offer1.IsDeleted = false;
                offer1.UpdatedIp = offer.UpdatedIp;
                offer1.UpdatedDate = offer.UpdatedDate;
                offer1.UpdatedBy = Environment.UserName;
                offer1.Product = new Product
                {
                    ProductName = name,
                    IsDeleted = offer.IsDeleted,
                    CreatedBy = Environment.UserName,
                    IsActive = offer.IsActive,
                    UpdatedBy = Environment.UserName
                };

                _context.Entry(offer1).State = EntityState.Modified;
                _context.Update(offer1);



                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "An error has occured");
            }

            return Ok();
        }

        [HttpPost("PostOffer")]
        public async Task<ActionResult<ProductsOffer>> CreateOfferForProduct([FromBody] ProductsOffer offer, string name)
        {
            ProductsOffer offer1 = new ProductsOffer();
            Product product = new Product();

            if (product.ProductName != name)
            {
                _logger.LogInformation($"Not found product: {product.ProductName}");
            }

            if (offer1 == null)
            {
                return StatusCode(404, "Offers not found");
            }

            offer1.Price = offer.Price;
            offer1.OfferId = offer.OfferId;
            offer1.IsActive = true;
            offer1.CreatedBy = Environment.UserName;
            offer1.CreatedDate = DateTime.Now;
            offer1.CreatedIp = offer.CreatedIp;
            offer1.IsDeleted = false;
            offer1.UpdatedIp = offer.UpdatedIp;
            offer1.UpdatedDate = offer.UpdatedDate;
            offer1.UpdatedBy = Environment.UserName;
            offer1.Product = new Product
            {
                ProductName = name,
                IsDeleted = offer.IsDeleted,
                CreatedBy = Environment.UserName,
                IsActive = offer.IsActive,
                UpdatedBy = Environment.UserName
            };

            try
            {
                _context.ProductsOffers.Add(offer1);
                _context.SaveChanges();
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal server error");

            }


            return Ok();
        }


        [HttpDelete("id")]
        public async Task<ActionResult<ProductsOffer>> DeleteOffer(Guid id)
        {
            if (id == null)
            {
                _logger.LogError($"Field is empty ");
            }

            try
            {
                var p = _context.ProductsOffers.FirstOrDefault(n => n.OfferId == id && n.IsActive == true);
                if (p == null)
                {
                    return StatusCode(404, "Products not found");
                }

                p.IsActive = false;
                _context.Entry(p).State = EntityState.Modified;
                _context.Update(p);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "An error has occured");
            }

            return Ok();
        }


        private bool OffersExists(string name)
        {
            return (_context.Products?.Any(e => e.ProductName == name)).GetValueOrDefault();
        }
    }
}
