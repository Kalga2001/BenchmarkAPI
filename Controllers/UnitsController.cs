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
    public class UnitsController : ControllerBase
    {
        private readonly ProductsDbContext _context;
        private ILogger<ProductsController> _logger;


        public UnitsController(ILogger<ProductsController> logger, ProductsDbContext context)
        {
            _logger = logger;
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            var allunits = _context.Units.Where(p => p.IsDeleted == false && p.IsActive == true)
                .Include(o => o.ProductsSizeOptions).ToListAsync();
            return await allunits;
        }


 
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnitByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Name is empty ");
            }


            var result1 = (from p in _context.Units
                           where p.UnitName == name && p.IsDeleted == false && p.IsActive != false
                           select p);
            try
            {
                var res = UnitExists(name);

                if (res != true)
                {
                    _logger.LogError($"Product with  UnitName: {name}, hasn't been found in db.");
                }
                else
                {
                    _logger.LogInformation($"Returned product with  UnitName: {name}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return await result1.Include(o => o.ProductsSizeOptions)
                 .ToListAsync();
        }


        [HttpPut("{name}")]
        public IActionResult UpdateUnit(Unit unit, string name)
        {
            try
            {
                var p = _context.Units.FirstOrDefault(n => n.UnitName == name && n.IsDeleted != true);
                if (p == null)
                {
                    return StatusCode(404, "Products not found");
                }

                p.UnitName = unit.UnitName;
                p.ProductsSizeOptions = unit.ProductsSizeOptions;
                p.UnitId = unit.UnitId;
                p.IsActive = unit.IsActive;
                p.CreatedBy = unit.CreatedBy;
                p.CreatedDate = unit.CreatedDate;
                p.CreatedIp = unit.CreatedIp;
                p.IsDeleted = unit.IsDeleted;
                p.UpdatedIp = unit.UpdatedIp;
                p.UpdatedDate = unit.UpdatedDate;
                p.UpdatedBy = unit.UpdatedBy;
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
        [HttpPost("CreateUnit")]
        public async Task<ActionResult<Unit>> CreateUnit([FromBody] Unit unit)
        {
            Unit unit1 = new Unit();

            if (unit == null)
            {
                return StatusCode(404, "Units not found");
            }

            unit1.UnitName = unit.UnitName;
            unit1.UnitId = Guid.NewGuid();
            unit1.IsActive = unit.IsActive;
            unit1.CreatedBy = Environment.UserName;
            unit1.CreatedDate = DateTime.Now;
            unit1.CreatedIp = unit.CreatedIp;
            unit1.IsDeleted = unit.IsDeleted;
            unit1.UpdatedIp = unit.UpdatedIp;
            unit1.UpdatedDate = unit.UpdatedDate;
            unit1.UpdatedBy = Environment.UserName;

            try
            {
                _context.Units.Add(unit1);
                _context.SaveChanges();
            }

            catch (Exception)
            {
                return StatusCode(500, "Internal server error");

            }

            return Ok();
        }

        [HttpDelete("name")]
        public async Task<ActionResult<Unit>> DeleteUnit(string name)
        {
            try
            {
                var p = _context.Units.FirstOrDefault(n => n.UnitName == name && n.IsActive == true);
                if (p == null)
                {
                    return StatusCode(404, "Units not found");
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


        private bool UnitExists(string name)
        {
            return (_context.Units?.Any(e => e.UnitName == name)).GetValueOrDefault();
        }


    }
}
