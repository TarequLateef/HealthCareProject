using AutoMapper;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Health.Motabea.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthClinic.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatMedicineController : ControllerBase
    {
        readonly IHealthUnits _huPM; readonly IMapper _mPM;
        public PatMedicineController(IHealthUnits huPM, IMapper mPM)
        { _huPM=huPM; _mPM=mPM; }

        [HttpGet("AllPatMed")]
        public async Task<IActionResult> AllPatMed(bool? aval,string? patMedID, string userID)
        {
            var userD = await _huPM.UserData.GetUserData(userID);
            try
            {
                IList<PatMed> prList = aval.HasValue ? aval.Value ?
                    await _huPM.PatMed.AvailableListAsync()
                    : await _huPM.PatMed.BannedListAsync()
                    : await _huPM.PatMed.Find(new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Medicine });
                prList = prList.Where(pr => pr.CompID==userD.CompID).ToList();
                if (!string.IsNullOrEmpty(patMedID))
                    prList=prList.Where(pr => pr.PatID==patMedID).ToList();
                return Ok(prList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatMedList")]
        public async Task<IActionResult> PatMedList(string patID, DateTime PatMedDate, string userID)
        {
            var userD = await _huPM.UserData.GetUserData(userID);
            try
            {
                IList<PatMed> prList = await _huPM.PatMed.SpecPatMed(patID, PatMedDate);
                return Ok(prList);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatMedDet")]
        public async Task<IActionResult> PatMedDet(string id, string userID)
        {
            var userD = await _huPM.UserData.GetUserData(userID);
            try
            {
                PatMed prItem = await _huPM.PatMed.GetByStringID(id);
                if (prItem is null) return NotFound();
                return Ok(await this.PatientMedlData(prItem));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpPost("AddPatMed")]
        public async Task<IActionResult> AddPatMed(PatMedDTO dTO)
        {
            var userD = await _huPM.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatMed prItem = new PatMed();
                _mPM.Map(dTO, prItem);
                PatMed newPR = await _huPM.PatMed.AddItem(prItem);
                await _huPM.SubmitAsync();
                newPR = await this.PatientMedlData(newPR);
                return Ok(newPR);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatMed")]
        public async Task<IActionResult> UpdatePatMed(PatMedDTO dTO)
        {
            var userD = await _huPM.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatMed prItem = await _huPM.PatMed.GetByStringID(dTO.PatMedID);
                if (prItem is null) return NotFound();
                _mPM.Map(dTO, prItem);
                PatMed upItem = _huPM.PatMed.Update(prItem);
                await _huPM.SubmitAsync();
                return Ok(await this.PatientMedlData(upItem));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("StopPatMed")]
        public async Task<IActionResult> StopPatMed(PatMedDTO dTO)
        {
            var userD = await _huPM.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatMed prItem = await _huPM.PatMed.GetByStringID(dTO.PatMedID);
                if (prItem is null) return NotFound();
                _mPM.Map(dTO, prItem);
                PatMed stopPr = await _huPM.PatMed.RestoreStop(prItem);
                await _huPM.SubmitAsync();
                return Ok(this.PatientMedlData(stopPr));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelPatMed")]
        public async Task<IActionResult> DelPatMed(string id, string userID)
        {
            var userD = await _huPM.UserData.GetUserData(userID);
            try
            {
                PatMed pbItem = await _huPM.PatMed.GetByStringID(id);
                if (pbItem is null) return NoContent();
                await _huPM.PatMed.Delete(id);
                await _huPM.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        private async Task<PatMed> PatientMedlData(PatMed pr)
        {
            pr.ParientDataTBL=await _huPM.PatientData.GetByStringID(pr.PatID);
            pr.BookingTBL= await _huPM.Booking.GetByStringID(pr.BookID);
            pr.MedicineTBL = await _huPM.Medicine.GetByStringID(pr.MedID);
            return pr;
        }
    }
}
