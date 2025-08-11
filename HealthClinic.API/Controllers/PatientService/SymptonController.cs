using AutoMapper;
using Health.Motabea.Core;
using Health.Motabea.Core.DTOs.Services;
using Health.Motabea.Core.Models.Services;
using Health.Motabea.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.API.Controllers.PatientService
{
    [Route("api/[controller]")]
    [ApiController]
    public class SymptonController : ControllerBase
    {
        readonly IHealthUnits _hUnit; readonly IMapper _hMap;
        public SymptonController(IHealthUnits healthUnits, IMapper mapper)
        { _hUnit = healthUnits; _hMap=mapper; }

        [HttpGet("AllSymptons")]
        public async Task<IActionResult> AllSymptons() => Ok(await _hUnit.Symptoms.GetAll());

        [HttpGet("SymptonDet")]
        public async Task<IActionResult> SymptonDet(string id)
        {
            try
            {
                Symptoms symItem = await _hUnit.Symptoms.GetByStringID(id);
                if (symItem is null) return NotFound();
                return Ok(symItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddSymp")]
        public async Task<IActionResult> AddSymp(SymptomsDTO dTO)
        {
            try
            {
                Symptoms symItem = new Symptoms();
                _hMap.Map(dTO, symItem);
                var newSymp = await _hUnit.Symptoms.AddItem(symItem);
                await _hUnit.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateSymp")]
        public async Task<IActionResult> UpdateSymp(SymptomsDTO dTO)
        {
            try
            {
                Symptoms sympItem = await _hUnit.Symptoms.GetByStringID(dTO.SymptomID);
                if (sympItem is null) return NotFound();
                _hMap.Map(dTO, sympItem);
                var upSymp = _hUnit.Symptoms.Update(sympItem);
                await _hUnit.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelSymp")]
        public async Task<IActionResult> DelSymp(string id)
        {
            try
            {
                Symptoms symItem = await _hUnit.Symptoms.GetByStringID(id);
                if (symItem is null) return NotFound();
                await _hUnit.Symptoms.Delete(id);
                await _hUnit.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
