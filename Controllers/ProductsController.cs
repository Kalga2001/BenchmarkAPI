using BenchmarkAPI.Common.Dtos;
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
            var allproduct = _context.Products.Where(p => p.IsDeleted != false && p.IsActive != false)
                .Include(o => o.ProductsOffers)
                 .ToListAsync();
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
                           where p.ProductName == name && p.IsDeleted != false && p.IsActive != false
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

        [HttpPut("UpdateProduct")]
        public IActionResult UpdateProduct(Product product, string name)
        {
            try
            {
                var p = _context.Products.FirstOrDefault(n => n.ProductName==name);
                if(p==null)
                {
                    return StatusCode(404, "Products not found");
                }

                p.ProductName = product.ProductName;
                p.ProductsOffers = product.ProductsOffers;
                p.ProductId = product.ProductId;
                p.IsActive = product.IsActive;
                p.CreateBy = product.CreateBy;
                p.CreateDate = product.CreateDate;
                p.CreatedIp = product.CreatedIp;
                p.IsDeleted = product.IsDeleted;
                p.UpdatedIp = product.UpdatedIp;
                p.UpdateDate=product.UpdateDate;
                p.UpdateBy=product.UpdateBy;
                _context.Entry(p).State = EntityState.Modified;
                _context.Update(p);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "An error has occured");
            }

            var products = _context.Products.ToList();

            return Ok(products);
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
                product1.ProductsOffers = product.ProductsOffers;
                product1.ProductId = new Guid();
                product1.IsActive = product.IsActive;
                product1.CreateBy = Environment.UserName;
                product1.CreateDate = DateTime.Now;
                product1.CreatedIp = product.CreatedIp;
                product1.IsDeleted = product.IsDeleted;
                product1.UpdatedIp = product.UpdatedIp;
                product1.UpdateDate = product.UpdateDate;
                product1.UpdateBy = Environment.UserName;
           
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
            }

            catch(Exception)
            {
                return StatusCode(500, "Internal server error");

            }

            return Ok();
         }


        private bool ProductExists(string name)
        {
            return (_context.Products?.Any(e => e.ProductName == name)).GetValueOrDefault();
        }


    }

}
