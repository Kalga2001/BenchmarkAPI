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
    public class MaterialsController : ControllerBase
    {
        private readonly ProductsDbContext _context;
        private ILogger<ProductsController> _logger;


        public MaterialsController(ILogger<ProductsController> logger, ProductsDbContext context)
        {
            _logger = logger;
            _context = context;
        }

 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterials()
        {
            var allmaterials = _context.Materials.Where(p => p.IsDeleted == false && p.IsActive == true)
                .Include(o => o.ProductsMaterialOptions).ToListAsync();
            return await allmaterials;
        }

 
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Material>>> GetMaterialByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Name is empty ");
            }


            var result1 = (from p in _context.Materials
                           where p.MaterialName == name && p.IsDeleted == false && p.IsActive != false
                           select p);
            try
            {
                var res = MaterialExists(name);

                if (res != true)
                {
                    _logger.LogError($"Product with  MaterialtName: {name}, hasn't been found in db.");
                }
                else
                {
                    _logger.LogInformation($"Returned product with  MaterialName: {name}");
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }

            return await result1.Include(o => o.ProductsMaterialOptions)
                 .ToListAsync();
        }

        //PUT: api/Products/5  //Update new and old name

        [HttpPut("{name}")]
        public IActionResult UpdateMaterial(Material material , string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                _logger.LogError($"Name is empty ");
            }

            try
            {
                var m = _context.Materials.FirstOrDefault(n => n.MaterialName == name && n.IsDeleted != true);
                if (m == null)
                {
                    return StatusCode(404, "Materials not found");
                }

                m.MaterialName = material.MaterialName;
                m.ProductsMaterialOptions = material.ProductsMaterialOptions;
                m.MaterialId = material.MaterialId;
                m.IsActive = material.IsActive;
                m.CreatedBy = material.CreatedBy;
                m.CreatedDate = material.CreatedDate;
                m.CreatedIp = material.CreatedIp;
                m.IsDeleted = material.IsDeleted;
                m.UpdatedIp = material.UpdatedIp;
                m.UpdatedDate = material.UpdatedDate;
                m.UpdatedBy = material.UpdatedBy;
                _context.Entry(m).State = EntityState.Modified;
                _context.Update(m);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "An error has occured");
            }

            return Ok();
        }

        [HttpPost("CreateMaterial")]
        public async Task<ActionResult<Material>> CreateMaterial([FromBody] Material material)
        {
            Material material1 = new Material();

            if (material1 == null)
            {
                return StatusCode(404, "Materials not found");
            }

            material1.MaterialName = material.MaterialName;
            material1.MaterialId = Guid.NewGuid();
            material1.IsActive = material.IsActive;
            material1.CreatedBy = Environment.UserName;
            material1.CreatedDate = DateTime.Now;
            material1.CreatedIp = material.CreatedIp;
            material1.IsDeleted = material.IsDeleted;
            material1.UpdatedIp = material.UpdatedIp;
            material1.UpdatedDate = material.UpdatedDate;
            material1.UpdatedBy = Environment.UserName;

            try
            {
                _context.Materials.Add(material1);
                _context.SaveChanges();
            }

            catch (Exception)
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
                var m = _context.Materials.FirstOrDefault(n => n.MaterialName == name && n.IsActive == true);
                if (m == null)
                {
                    return StatusCode(404, "Materials not found");
                }

                m.IsActive = false;
                _context.Entry(m).State = EntityState.Modified;
                _context.Update(m);
                _context.SaveChanges();
            }
            catch
            {
                return StatusCode(500, "An error has occured");
            }

            return Ok();
        }


        private bool MaterialExists(string name)
        {
            return (_context.Materials?.Any(e => e.MaterialName== name)).GetValueOrDefault();
        }


    }
}
