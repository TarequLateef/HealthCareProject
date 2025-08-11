using AutoMapper;
using Health.Motabea.Core;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Runtime.CompilerServices;

namespace HealthClinic.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientDetailsController : ControllerBase
    {
        readonly IHealthUnits _hu; IMapper _huMap;
        public PatientDetailsController(IHealthUnits hu, IMapper huMap) { _hu=hu; _huMap=huMap; }

        [HttpGet("AllPatients")]
        public async Task<IActionResult> AllPatients() =>
            Ok(await _hu.PatientData.GetAll());

        [HttpGet("PatientDatas")]
        public async Task<IActionResult> PatientDatas(string id)
        {
            try
            {
                PatientData pdItem = await _hu.PatientData.GetByStringID(id);
                if (pdItem is null) return Ok(new PatientData());
                pdItem.PatientBaseTBL=await _hu.PatientBaseData.GetByStringID(pdItem.PatientID);
                return Ok(pdItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient(PatientDataDto dto)
        {
            try
            {
                PatientData pd = new PatientData();
                _huMap.Map(dto, pd);
                var pdItem = await _hu.PatientData.AddItem(pd);
                _hu.Submit();
                pdItem.PatientBaseTBL = await _hu.PatientBaseData.GetByStringID(pdItem.PatientID);
                return Ok(pdItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("EditPatient")]
        public async Task<IActionResult> EditPatient(PatientDataDto dto)
        {
            try
            {
                var pdItem = await _hu.PatientData.GetByStringID(dto.PatientID);
                if (pdItem is null) return NotFound();
                _huMap.Map(dto, pdItem);
                var upItem = _hu.PatientData.Update(pdItem);
                await _hu.SubmitAsync();
                upItem.PatientBaseTBL = await _hu.PatientBaseData.GetByStringID(pdItem.PatientID);
                return Ok(upItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelPat")]
        public async Task<IActionResult> DelPat(string id)
        {
            try
            {
                await _hu.PatientData.Delete(id);
                _hu.Submit();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
