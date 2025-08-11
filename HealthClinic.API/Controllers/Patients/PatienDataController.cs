using AutoMapper;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatienDataController : ControllerBase
    {
        readonly IHealthUnits _hu; IMapper _huMap;
        public PatienDataController(IHealthUnits healthUnits, IMapper mapper) 
        { _hu = healthUnits; _huMap = mapper; }

        [HttpGet("AllPatients")]
        public async Task<IActionResult> AllPatients() =>
            Ok(await _hu.PatientBaseData.GetAll());

        [HttpGet("PatienDetails")]
        public async Task<IActionResult> PatienDetails(string id)
        {
            try
            {
                var pdItem = await _hu.PatientBaseData.GetByStringID(id);
                if (pdItem is null) return NotFound();
                return Ok(pdItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatientByPhone")]
        public async Task<IActionResult> PatientByPhone(string Pphone)
        {
            try
            {
                ReturnState<PatientBaseData> pdItem = await _hu.PatientBaseData.ByPhone(Pphone);
                return StatusCode(StatusCodes.Status200OK, pdItem);
            }
            catch { return StatusCode(StatusCodes.Status200OK, _hu.PatientBaseData.ErrorState()); }
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient(PatientBaseDTO dto)
        {
            try
            {
                PatientBaseData pd = new PatientBaseData();
                _huMap.Map(dto, pd);
                var item = await _hu.PatientBaseData.AddItem(pd);
                _hu.Submit();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> UpdatePatient(PatientBaseDTO dTO)
        {
            try
            {
                PatientBaseData pdItem = await _hu.PatientBaseData.GetByStringID(dTO.PatientID);
                _huMap.Map(dTO, pdItem);
                var item = _hu.PatientBaseData.Update(pdItem);
                _hu.Submit();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DeletePatient")]
        public async Task<IActionResult> DeletePatient(string id)
        {
            try
            {
                PatientBaseData pdItem = await _hu.PatientBaseData.GetByStringID(id);
                _hu.PatientBaseData.Delete(pdItem);
                _hu.Submit();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
