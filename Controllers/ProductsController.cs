using BenchmarkAPI.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;

namespace BenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext _context;
        private ILogger<ProductsController> _logger;


        public ProductsController(ILogger<ProductsController> logger, ProductsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Products

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var allproduct = _context.Products.Where(p => p.IsDeleted == false && p.IsActive == true)
                .Include(o => o.ProductsOffers).ToListAsync();
            return await allproduct;
        }


        // GET: api/Products/5     
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Name is empty ");
            }


            var result1 = (from p in _context.Products
                           where p.ProductName == name && p.IsDeleted == false && p.IsActive != false
                           select p);
            try
            {
                var res = ProductExists(name);

                if (res != true)
                {
                    _logger.LogError($"Product with ProductName: {name}, hasn't been found in db.");
                }
                else
                {
                    _logger.LogInformation($"Returned product with ProductName: {name}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return await result1.Include(o => o.ProductsOffers)
                 .ToListAsync();
        }

        //PUT: api/Products/5  //Update new and old name

        [HttpPut("{name}")]
        public IActionResult UpdateProduct(Product product, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Name is empty ");
            }

            try
            {
                var p = _context.Products.FirstOrDefault(n => n.ProductName==name && n.IsDeleted != true);
                if(p==null)
                {
                    return StatusCode(404, "Products not found");
                }

                p.ProductName = product.ProductName;
                p.ProductsOffers = product.ProductsOffers;
                p.ProductId = product.ProductId;
                p.IsActive = true;
                p.CreatedBy = Environment.UserName;
                p.CreatedDate = DateTime.Now;
                p.CreatedIp = product.CreatedIp;
                p.IsDeleted = false;
                p.UpdatedIp = product.UpdatedIp;
                p.UpdatedDate = product.UpdatedDate;
                p.UpdatedBy = Environment.UserName;

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



        // POST: api/Products
        [HttpPost("CreateProduct")]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
                Product product1 = new Product();

                if (product1 == null)
                {
                    return StatusCode(404, "Products not found");
                }

                product1.ProductName = product.ProductName;
                product1.ProductId = Guid.NewGuid();
                product1.IsActive = true;
                product1.CreatedBy = Environment.UserName;
                product1.CreatedDate = DateTime.Now;
                product1.CreatedIp = product.CreatedIp;
                product1.IsDeleted = false;
                product1.UpdatedIp = product.UpdatedIp;
                product1.UpdatedDate = product.UpdatedDate;
                product1.UpdatedBy = Environment.UserName;



            try
            {
                _context.Products.Add(product1);
                _context.SaveChanges();
            }

            catch(Exception)
            {
                return StatusCode(500, "Internal server error");

            }

            return Ok();
         }

        [HttpDelete("name")]
        public async Task<ActionResult<Product>> DeleteProduct(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Name is empty ");
            }

            try
            {
                var p = _context.Products.FirstOrDefault(n => n.ProductName == name && n.IsActive==true);
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


        private bool ProductExists(string name)
        {
            return (_context.Products?.Any(e => e.ProductName == name)).GetValueOrDefault();
        }


    }

}
