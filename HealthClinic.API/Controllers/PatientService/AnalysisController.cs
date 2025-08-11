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
    public class AnalysisController : ControllerBase
    {
        readonly IHealthUnits _analUint; readonly IMapper _analMap;
        public AnalysisController(IHealthUnits healthUnits, IMapper mapper)
        { _analUint = healthUnits; _analMap=mapper; }

        [HttpGet("AllAnalysis")]
        public async Task<IActionResult> AllAnalysis() => Ok(await _analUint.Analysis.GetAll());

        [HttpGet("AnalysisDet")]
        public async Task<IActionResult> AnalysisDet(string id)
        {
            try
            {
                Analysis item = await _analUint.Analysis.GetByStringID(id);
                if (item is null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddAnal")]
        public async Task<IActionResult> AddAnal(AnalysisDTO dTO)
        {
            try
            {
                Analysis item = new Analysis();
                _analMap.Map(dTO, item);
                Analysis newSymp = await _analUint.Analysis.AddItem(item);
                await _analUint.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateAnal")]
        public async Task<IActionResult> UpdateAnal(AnalysisDTO dTO)
        {
            try
            {
                Analysis item = await _analUint.Analysis.GetByStringID(dTO.AnalysisID);
                if (item is null) return NotFound();
                _analMap.Map(dTO, item);
                Analysis upSymp = _analUint.Analysis.Update(item);
                await _analUint.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelAnal")]
        public async Task<IActionResult> DelAnal(string id)
        {
            try
            {
                Analysis item = await _analUint.Analysis.GetByStringID(id);
                if (item is null) return NotFound();
                await _analUint.Analysis.Delete(id);
                await _analUint.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
