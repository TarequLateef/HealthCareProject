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
    public class CTController : ControllerBase
    {
        readonly IHealthUnits _ctUint; readonly IMapper _ctMap;
        public CTController(IHealthUnits healthUnits, IMapper mapper)
        { _ctUint = healthUnits; _ctMap=mapper; }

        [HttpGet("AllCT")]
        public async Task<IActionResult> AllCT() => Ok(await _ctUint.CT.GetAll());

        [HttpGet("CTDet")]
        public async Task<IActionResult> CTDet(string id)
        {
            try
            {
                CT item = await _ctUint.CT.GetByStringID(id);
                if (item is null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddCT")]
        public async Task<IActionResult> AddCT(CT_DTO dTO)
        {
            try
            {
                CT item = new CT();
                _ctMap.Map(dTO, item);
                CT newSymp = await _ctUint.CT.AddItem(item);
                await _ctUint.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateCT")]
        public async Task<IActionResult> UpdateCT(CT_DTO dTO)
        {
            try
            {
                CT item = await _ctUint.CT.GetByStringID(dTO.CT_ID);
                if (item is null) return NotFound();
                _ctMap.Map(dTO, item);
                CT upSymp = _ctUint.CT.Update(item);
                await _ctUint.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelCT")]
        public async Task<IActionResult> DelCT(string id)
        {
            try
            {
                CT item = await _ctUint.CT.GetByStringID(id);
                if (item is null) return NotFound();
                await _ctUint.CT.Delete(id);
                await _ctUint.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
