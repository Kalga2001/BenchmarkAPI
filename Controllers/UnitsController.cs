using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BenchmarkAPI.DAL;
using System.Net;
using BenchmarkAPI.Common.ResultDtos.UnitsDto;

namespace BenchmarkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {

        private ILogger<UnitsController> _logger;


        public UnitsController(ILogger<UnitsController> logger)
        {
            _logger = logger;
  
        }


        [HttpGet("GetAll")]
        public async Task<GetUnitsResultDto> GetUnits()
        {
            var result = new GetUnitsResultDto();

            try
            {
                using (var _context = new ProductsDbContext())
                {
                    List<Unit> allUnits = await _context.Units.Where(p => p.IsDeleted == false && p.IsActive == true).ToListAsync();

                    if (allUnits.Any())
                    {
                        result.Units = allUnits;
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
                _logger.LogError("[{1}]:Error in Get Units{2}.", DateTime.Now, ex.InnerException);
            }
            return result;
        }


        [HttpGet]
        public async Task<GetUnitsResultDto> GetUnitByName(string name)
        {
            var result = new GetUnitsResultDto();

            if (!ModelState.IsValid)
            {
                result.Status = "No Completed";
                result.Code = 400;
            }

            try
            {
                using (var _context = new ProductsDbContext())
                {

                    var unit = (from p in _context.Units
                                where p.UnitName == name && p.IsDeleted == false && p.IsActive != false
                                select p);

                    if (unit.Count() > 0)
                    {
                        result.Units = unit.ToList();
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
                _logger.LogError("[{1}]:Error in Get Units By Name {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPut]
        public async Task<UpdateUnitResultDto> UpdateUnitByName (string oldName, string newName)
        {
            var result = new UpdateUnitResultDto();
            
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

                    var unit = _context.Units.First(n => n.UnitName == oldName && n.IsDeleted != true);


                    if (unit != null)
                    {

                        unit.UnitName = newName;
                        unit.UpdatedIp = Dns.GetHostName();
                        unit.UpdatedDate = DateTime.Now;
                        unit.UpdatedBy = Environment.UserName;

                        _context.Entry(unit).State = EntityState.Modified;
                        _context.Update(unit);
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
                _logger.LogError("[{1}]:Error in Update Unit {2}.", DateTime.Now, ex.InnerException);
            }
            return result;

        }



        [HttpPost]
        public async Task<CreateUnitResultDto> CreateUnitByName(string name)
        {
            var result = new CreateUnitResultDto();

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
                    Unit unit =new Unit();

                  
                        unit.UnitName = name;
                        unit.UnitId = Guid.NewGuid();
                        unit.IsActive = true;
                        unit.CreatedBy = Environment.UserName;
                        unit.CreatedDate = DateTime.Now;
                        unit.CreatedIp = Dns.GetHostName();
                        unit.IsDeleted = false;

                        result.Status = "Created";
                        result.Code = 201;
                        result.IsCreated = true;
                        _context.Units.Add(unit);
                        _context.SaveChanges();
                    
                   
                }
            }

            catch (Exception ex)
            {
                result.Status = ex.InnerException.ToString();
                result.Code = 0;
                result.IsCreated = false;
                _logger.LogError("[{1}]:Error in Create Unit {2}.", DateTime.Now, ex.InnerException);

            }

            return result;
        }



        [HttpDelete]
        public async Task<DeleteUnitResultDto> DeleteUnitByName(string name)
        {

            var result = new DeleteUnitResultDto();

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

                    var unit = _context.Units.First(n => n.UnitName == name && n.IsActive == true);

                    if (unit != null)
                    {
                        unit.IsActive = false;
                        unit.IsDeleted = true;

                        result.Status = "Deleted";
                        result.Code = 200;
                        result.IsDeleted = true;
                        _context.Entry(unit).State = EntityState.Modified;
                        _context.Units.Update(unit);
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
