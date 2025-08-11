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
    public class DiagController : ControllerBase
    {
        readonly IHealthUnits _diagUint; readonly IMapper _diagMap;
        public DiagController(IHealthUnits healthUnits, IMapper mapper)
        { _diagUint = healthUnits; _diagMap=mapper; }

        [HttpGet("AllDiagnostic")]
        public async Task<IActionResult> AllDiagnostic() => Ok(await _diagUint.Diagnositic.GetAll());

        [HttpGet("DiagnosticDet")]
        public async Task<IActionResult> DiagnosticDet(string id)
        {
            try
            {
                Diagnostic item = await _diagUint.Diagnositic.GetByStringID(id);
                if (item is null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddDiagnostic")]
        public async Task<IActionResult> AddDiagnostic(DiagnosticDTO dTO)
        {
            try
            {
                Diagnostic item = new Diagnostic();
                _diagMap.Map(dTO, item);
                Diagnostic newSymp = await _diagUint.Diagnositic.AddItem(item);
                await _diagUint.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateDiagnostic")]
        public async Task<IActionResult> UpdateDiagnostic(DiagnosticDTO dTO)
        {
            try
            {
                Diagnostic item = await _diagUint.Diagnositic.GetByStringID(dTO.DiagID);
                if (item is null) return NotFound();
                _diagMap.Map(dTO, item);
                Diagnostic upSymp = _diagUint.Diagnositic.Update(item);
                await _diagUint.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelDiagnostic")]
        public async Task<IActionResult> DelDiagnostic(string id)
        {
            try
            {
                Diagnostic item = await _diagUint.Diagnositic.GetByStringID(id);
                if (item is null) return NotFound();
                await _diagUint.Diagnositic.Delete(id);
                await _diagUint.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
