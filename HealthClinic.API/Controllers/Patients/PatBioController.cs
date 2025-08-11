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
    public class PatBioController : ControllerBase
    {
        readonly IHealthUnits _huPB; readonly IMapper _mPB;
        public PatBioController(IHealthUnits huPB, IMapper mPB)
        { _huPB=huPB; _mPB=mPB; }
        [HttpGet("AllPatientBios")]
        public async Task<IActionResult> AllPatientBios(bool? aval,string? patID, string userID)
        {
            var userD = await _huPB.UserData.GetUserData(userID);

            try
            {
                var pbList = aval.HasValue ? aval.Value ?
                    await _huPB.PatBio.AvailableListAsync()
                    : await _huPB.PatBio.BannedListAsync()
                    : await _huPB.PatBio.Find(new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Biometirc });
                pbList = pbList.Where(pb => pb.CompID==userD.CompID).ToList();
                if (!string.IsNullOrEmpty(patID))
                    pbList=pbList.Where(pb => pb.PatID==patID).ToList();
                return Ok(pbList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatientBioDetails")]
        public async Task<IActionResult> PatientBioDetails(string id,string userID)
        {
            var userD = await _huPB.UserData.GetUserData(userID);
            try
            {
                var item = await this.pbItemData(id);
                return Ok(item);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddPatBio")]
        public async Task<IActionResult> AddPatBio(PatBioDTO dTO)
        {
            var userD = await _huPB.UserData.GetUserData(dTO.PatBioID);
            try
            {
                var pbItem = new PatientBio();
                _mPB.Map(dTO, pbItem);
                var newPB = await _huPB.PatBio.AddItem(pbItem);
                await _huPB.SubmitAsync();
                newPB = await this.pbItemData(newPB.PatBioID);
                return Ok(newPB);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatBio")]
        public async Task<IActionResult> UpdatePatBio(PatBioDTO dTO)
        {
            var userD = await _huPB.UserData.GetUserData(dTO.PatBioID);
            try
            {
                var pbItem = await _huPB.PatBio.GetByStringID(dTO.PatBioID);
                if (pbItem is null) return NotFound();
                _mPB.Map(dTO, pbItem);
                var upItem = _huPB.PatBio.Update(pbItem); 
                await _huPB.SubmitAsync();
                upItem = await this.pbItemData(upItem.PatBioID);
                return Ok(upItem);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("StopPatBio")]
        public async Task<IActionResult> StopPatBio(PatBioDTO dTO)
        {
            var userD = await _huPB.UserData.GetUserData(dTO.PatBioID);
            try
            {
                var pbItem = await _huPB.PatBio.GetByStringID(dTO.PatBioID);
                if (pbItem is null) return NotFound();
                _mPB.Map(dTO, pbItem);
                var stopItem = await _huPB.PatBio.RestoreStop(pbItem);
                await _huPB.SubmitAsync();
                return Ok(this.pbItemData(stopItem.PatBioID));
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelPatBio")]
        public async Task<IActionResult> DelPatBio(string id,string userID)
        {
            var userD = await _huPB.UserData.GetUserData(userID);
            try
            {
                var pbItem = await _huPB.PatBio.GetByStringID(id);
                if (pbItem is null) return NoContent();
                await _huPB.PatBio.Delete(id);
                await _huPB.SubmitAsync();
                return Ok();
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        private async Task<PatientBio> pbItemData(string id)
        {
            var pbItem = await _huPB.PatBio.GetByStringID(id);
            if (pbItem is null) return new PatientBio();
            pbItem.BiometricTBL = await _huPB.Biometric.GetByStringID(pbItem.BioID);
            pbItem.BookingTBL = await _huPB.Booking.GetByStringID(pbItem.BookID);
            pbItem.ParientDataTBL = await _huPB.PatientData.GetByStringID(pbItem.PatID);
            return pbItem;
        }
    }
}