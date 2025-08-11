using AutoMapper;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using Health.Motabea.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.API.Controllers.PatientService
{
    [Route("api/[controller]")]
    [ApiController]
    public class RaysController : ControllerBase
    {
        readonly IHealthUnits _rayUint; readonly IMapper _rayMap;
        public RaysController(IHealthUnits healthUnits, IMapper mapper)
        { _rayUint = healthUnits; _rayMap=mapper; }

        [HttpGet("AllRays")]
        public async Task<IActionResult> AllRays() => Ok(await _rayUint.Rays.GetAll());

        [HttpGet("RaysDet")]
        public async Task<IActionResult> RaysDet(string id)
        {
            try
            {
                Rays item = await _rayUint.Rays.GetByStringID(id);
                if (item is null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddRays")]
        public async Task<IActionResult> AddRays(RaysDTO dTO)
        {
            try
            {
                Rays item = new Rays();
                _rayMap.Map(dTO, item);
                Rays newSymp = await _rayUint.Rays.AddItem(item);
                await _rayUint.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateRays")]
        public async Task<IActionResult> UpdateRays(RaysDTO dTO)
        {
            try
            {
                Rays item = await _rayUint.Rays.GetByStringID(dTO.RayID);
                if (item is null) return NotFound();
                _rayMap.Map(dTO, item);
                Rays upSymp = _rayUint.Rays.Update(item);
                await _rayUint.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelRays")]
        public async Task<IActionResult> DelRays(string id)
        {
            try
            {
                Rays item = await _rayUint.Rays.GetByStringID(id);
                if (item is null) return NotFound();
                await _rayUint.Rays.Delete(id);
                await _rayUint.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
