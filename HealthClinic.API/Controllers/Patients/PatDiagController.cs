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
    public class PatDiagController : ControllerBase
    {
        readonly IHealthUnits _huPD; readonly IMapper _mPD;
        public PatDiagController(IHealthUnits huPD, IMapper mPD)
        { _huPD=huPD; _mPD=mPD; }

        [HttpGet("AllPatDiags")]
        public async Task<IActionResult> AllPatDiags(bool? aval, string userID)
        {
            var userD = await _huPD.UserData.GetUserData(userID);
            try
            {
                var pdList = aval.HasValue ? aval.Value ?
                   await _huPD.PatientDiag.AvailableListAsync()
                   : await _huPD.PatientDiag.BannedListAsync()
                   : await _huPD.PatientDiag.Find(new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Diagnostic });
                pdList = pdList.Where(pd => pd.CompID==userD.CompID).ToList();
                return Ok(pdList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatDiagDet")]
        public async Task<IActionResult> PatDiagDet(string id, string userID)
        {
            var userD = await _huPD.UserData.GetUserData(userID);
            try
            {
                var patDiag = await _huPD.PatientDiag.GetByStringID(id);
                patDiag = await this.PatDiagData(patDiag);
                return Ok(patDiag);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddPatDiag")]
        public async Task<IActionResult> AddPatDiag(PatientDiagDTO dto)
        {
            var userD = await _huPD.UserData.GetUserData(dto.UserLogID);
            try
            {
                PatientDiag pd = new PatientDiag();
                _mPD.Map(dto, pd);
                var pdItem = await _huPD.PatientDiag.AddItem(pd);
                await _huPD.SubmitAsync();
                pdItem = await this.PatDiagData(pdItem);
                return Ok(pdItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatDiag")]
        public async Task<IActionResult> UpdatePatDiag(PatientDiagDTO dTO)
        {
            var userD = await _huPD.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatientDiag patDiagItem = await _huPD.PatientDiag.GetByStringID(dTO.PatDiagID);
                if (patDiagItem is null) return NotFound();
                _mPD.Map(dTO, patDiagItem);
                var upPD = _huPD.PatientDiag.Update(patDiagItem);
                await _huPD.SubmitAsync();
                upPD = await this.PatDiagData(upPD);
                return Ok(upPD);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("RestorePatDiag")]
        public async Task<IActionResult> RestorePatDiag(PatientDiagDTO dTO)
        {
            var userD = await _huPD.UserData.GetUserData(dTO.UserLogID);
            try
            {
                var pdItem = await _huPD.PatientDiag.GetByStringID(dTO.PatDiagID);
                if (pdItem is null) return NotFound();
                _mPD.Map(dTO, pdItem);
                var stopPD = await _huPD.PatientDiag.RestoreStop(pdItem);
                await _huPD.SubmitAsync();
                stopPD = await this.PatDiagData(stopPD);
                return Ok(stopPD);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DeletePd")]
        public async Task<IActionResult> DeletePd(string id, string userID)
        {
            var userD = await _huPD.UserData.GetUserData(userID);
            try
            {
                var pdItem = await _huPD.PatientDiag.GetByStringID(id);
                if (pdItem is null) return NotFound();
                await _huPD.PatientDiag.Delete(id);
                _huPD.Submit();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        private async Task<PatientDiag> PatDiagData(PatientDiag pd)
        {
            pd.ParientDataTBL= await _huPD.PatientData.GetByStringID(pd.PatID);
            pd.BookingTBL=await _huPD.Booking.GetByStringID(pd.BookingID);
            pd.DiagnosticTBL = await _huPD.Diagnositic.GetByStringID(pd.DiagID);
            return pd;
        }
    }
}
