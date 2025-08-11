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
    public class PatientSympController : ControllerBase
    {
        readonly IHealthUnits _hUnit; IMapper _hMap;
        public PatientSympController(IHealthUnits healthUnits, IMapper mapper)
        {
            _hUnit=healthUnits; _hMap=mapper;
        }

        [HttpGet("AllPatSympo")]
        public async Task<IActionResult> AllPatSympo(bool? aval, string? PatSymp)
        {
            try
            {
                var patSympList = aval.HasValue ? aval.Value ?
                    await _hUnit.PatientSymp.AvailableListAsync()
                    : await _hUnit.PatientSymp.BannedListAsync()
                    : await _hUnit.PatientSymp.Find(new[] { PatientTab.Patient, ServTab.Symptoms, PatientTab.Booking });
                if (!string.IsNullOrWhiteSpace(PatSymp))
                    patSympList = patSympList.Where(ps => ps.PatID==PatSymp).ToList();
                return Ok(patSympList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatSympDet")]
        public async Task<IActionResult> PatSympDet(string id)
        {
            try
            {
                PatientSymptom patSympItem = await _hUnit.PatientSymp.GetByStringID(id);
                if (patSympItem is null) return NotFound();
                patSympItem = await this.PatSympData(patSympItem);
                return Ok(patSympItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddPatSymp")]
        public async Task<IActionResult> AddPatSymp(PatSympDTO dTO)
        {
            try
            {
                PatientSymptom patItem = new PatientSymptom();
                _hMap.Map(dTO, patItem);
                var newPatSymp = await _hUnit.PatientSymp.AddItem(patItem);
                await _hUnit.SubmitAsync();
                return Ok(await this.PatSympData(newPatSymp));
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatSymp")]
        public async Task<IActionResult> UpdatePatSymp(PatSympDTO dTO)
        {
            try
            {
                PatientSymptom patSymItem = await _hUnit.PatientSymp.GetByStringID(dTO.PatSympID);
                if (patSymItem is null) return NotFound();
                _hMap.Map(dTO, patSymItem);
                var upPatSym = _hUnit.PatientSymp.Update(patSymItem);
                await _hUnit.SubmitAsync();
                return Ok(await this.PatSympData(upPatSym));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("RestStopPatSymp")]
        public async Task<IActionResult> RestStopPatSymp(PatSympDTO dTO)
        {
            try
            {
                PatientSymptom patSymItem = await _hUnit.PatientSymp.GetByStringID(dTO.PatSympID);
                if (patSymItem is null) return NotFound();
                _hMap.Map(dTO, patSymItem);
                var stopPatSym = await _hUnit.PatientSymp.RestoreStop(patSymItem);
                await _hUnit.SubmitAsync();
                return Ok(this.PatSympData(stopPatSym));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelPatSymp")]
        public async Task<IActionResult> DelPatSymp(string id)
        {
            try
            {
                PatientSymptom patSymItem = await _hUnit.PatientSymp.GetByStringID(id);
                if (patSymItem is null) return NotFound();
                _hUnit.PatientSymp.Delete(patSymItem);
                await _hUnit.SubmitAsync();
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        private async Task<PatientSymptom> PatSympData(PatientSymptom Item)
        {
            Item.ParientDataTBL=await _hUnit.PatientData.GetByStringID(Item.PatID);
            Item.SymptomsTBL = await _hUnit.Symptoms.GetByStringID(Item.SympID);
            Item.BookingTBL = await _hUnit.Booking.GetByStringID(Item.BookID);
            return Item;
        } }
}
