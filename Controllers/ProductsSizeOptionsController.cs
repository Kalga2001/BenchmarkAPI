using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenchmarkAPI.DAL;

namespace BenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsSizeOptionsController : ControllerBase
    {
        private readonly ProductsDbContext _context;

        public ProductsSizeOptionsController(ProductsDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductsSizeOptions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsSizeOption>>> GetProductsSizeOptions()
        {
          if (_context.ProductsSizeOptions == null)
          {
              return NotFound();
          }
            return await _context.ProductsSizeOptions.ToListAsync();
        }

        // GET: api/ProductsSizeOptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsSizeOption>> GetProductsSizeOption(Guid id)
        {
          if (_context.ProductsSizeOptions == null)
          {
              return NotFound();
          }
            var productsSizeOption = await _context.ProductsSizeOptions.FindAsync(id);

            if (productsSizeOption == null)
            {
                return NotFound();
            }

            return productsSizeOption;
        }

        // PUT: api/ProductsSizeOptions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductsSizeOption(Guid id, ProductsSizeOption productsSizeOption)
        {
            if (id != productsSizeOption.SizeOptionId)
            {
                return BadRequest();
            }

            _context.Entry(productsSizeOption).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductsSizeOptionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProductsSizeOptions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductsSizeOption>> PostProductsSizeOption(ProductsSizeOption productsSizeOption)
        {
          if (_context.ProductsSizeOptions == null)
          {
              return Problem("Entity set 'ProductsDbContext.ProductsSizeOptions'  is null.");
          }
            _context.ProductsSizeOptions.Add(productsSizeOption);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductsSizeOption", new { id = productsSizeOption.SizeOptionId }, productsSizeOption);
        }

        // DELETE: api/ProductsSizeOptions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductsSizeOption(Guid id)
        {
            if (_context.ProductsSizeOptions == null)
            {
                return NotFound();
            }
            var productsSizeOption = await _context.ProductsSizeOptions.FindAsync(id);
            if (productsSizeOption == null)
            {
                return NotFound();
            }

            _context.ProductsSizeOptions.Remove(productsSizeOption);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductsSizeOptionExists(Guid id)
        {
            return (_context.ProductsSizeOptions?.Any(e => e.SizeOptionId == id)).GetValueOrDefault();
        }
    }
}
