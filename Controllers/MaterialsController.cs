using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenchmarkAPI.DAL;
using System.Net;
using BenchmarkAPI.Common.ResultDtos.MaterialsDto;

namespace BenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {

        private ILogger<MaterialsController> _logger;


        public MaterialsController(ILogger<MaterialsController> logger)
        {
            _logger = logger;

        }


        [HttpGet("GetAll")]
        public async Task<GetMaterialResultDto> GetMaterials()
        {
            var result = new GetMaterialResultDto();

            try
            {
                using (var _context = new ProductsDbContext())
                {
                    List<Material> allMaterials = await _context.Materials.Where(p => p.IsDeleted == false && p.IsActive == true).ToListAsync();

                    if (allMaterials.Any())
                    {
                        result.Materials = allMaterials;
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
                _logger.LogError("[{1}]:Error in Get Materials {2}.", DateTime.Now, ex.InnerException);
            }
            return result;
        }


        [HttpGet]
        public async Task<GetMaterialResultDto> GetMaterialByName(string name)
        {
            
                var result = new GetMaterialResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Completed";
                result.Code = 400;
                result.Materials = null;
            }


            try
            {
                using (var _context = new ProductsDbContext())
                {



                    var material = (from p in _context.Materials
                                    where p.MaterialName == name && p.IsDeleted == false && p.IsActive != false
                                    select p);

                    if (material.Count() > 0)
                    {
                        result.Materials = material.ToList();
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
        public async Task<UpdateMaterialResultDto> UpdateMaterialByName(string oldName, string newName)
        {

            var result = new UpdateMaterialResultDto();

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

                    var material = _context.Materials.First(n => n.MaterialName == oldName && n.IsDeleted != true);


                    if (material != null)
                    {


                        material.MaterialName = newName;
                        material.UpdatedIp = Dns.GetHostName();
                        material.UpdatedDate = DateTime.Now;
                        material.UpdatedBy = Environment.UserName;




                        _context.Entry(material).State = EntityState.Modified;
                        _context.Update(material);
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
                _logger.LogError("[{1}]:Error in Update Material {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPost]
        public async Task<CreateMaterialResultDto> CreateMaterialByName(string name)
        {
            var result = new CreateMaterialResultDto();

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
                    Material material = new Material();
                    material.MaterialName = name;
                    material.MaterialId = Guid.NewGuid();
                    material.IsActive = true;
                    material.CreatedBy = Environment.UserName;
                    material.CreatedDate = DateTime.Now;
                    material.CreatedIp = Dns.GetHostName();
                    material.IsDeleted = false;

                    result.Status = "Created";
                    result.Code = 201;
                    result.IsCreated = true;
                    _context.Materials.Add(material);
                    _context.SaveChanges();

                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsCreated = false;
                _logger.LogError("[{1}]:Error in Create Material {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }



        [HttpDelete]
        public async Task<DeleteMaterialResultDto> DeleteMaterialByName(string name)
        {

            var result = new DeleteMaterialResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Valid";
                result.Code = 400;
                result.IsDeleted = false;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var material = _context.Materials.First(n => n.MaterialName == name && n.IsActive == true);


                    if (material != null)
                    {
                        material.IsActive = false;
                        material.IsDeleted = true;

                        result.Status = "Deleted";
                        result.Code = 200;
                        result.IsDeleted = true;

                        _context.Entry(material).State = EntityState.Modified;
                        _context.Materials.Update(material);
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
                _logger.LogError("[{1}]:Error in Delete Material {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }


    }
}
