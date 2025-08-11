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
    public class PatCtController : ControllerBase
    {
        readonly IHealthUnits _huPCt; readonly IMapper _mPCt;
        public PatCtController(IHealthUnits huPCt, IMapper mPCt)
        { _huPCt=huPCt; _mPCt=mPCt; }

        [HttpGet("AllPatCt")]
        public async Task<IActionResult> AllPatCt(bool? aval, string? patCtID, string userID)
        {
            var userD = await _huPCt.UserData.GetUserData(userID);
            try
            {
                IList<PatCt> prList = aval.HasValue ? aval.Value ?
                    await _huPCt.PatCt.AvailableListAsync()
                    : await _huPCt.PatCt.BannedListAsync()
                    : await _huPCt.PatCt.Find(new[] { PatientTab.Patient, PatientTab.Booking, ServTab.CT });
                prList = prList.Where(pr => pr.CompID==userD.CompID).ToList();
                if (!string.IsNullOrEmpty(patCtID))
                    prList=prList.Where(pr => pr.PatID==patCtID).ToList();
                return Ok(prList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatCtDet")]
        public async Task<IActionResult> PatCtDet(string id, string userID)
        {
            var userD = await _huPCt.UserData.GetUserData(userID);
            try
            {
                PatCt prItem = await _huPCt.PatCt.GetByStringID(id);
                if (prItem is null) return NotFound();
                return Ok(await this.PatientMedlData(prItem));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpPost("AddPatCt")]
        public async Task<IActionResult> AddPatCt(PatCtDTO dTO)
        {
            var userD = await _huPCt.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatCt prItem = new PatCt();
                _mPCt.Map(dTO, prItem);
                PatCt newPR = await _huPCt.PatCt.AddItem(prItem);
                await _huPCt.SubmitAsync();
                newPR = await this.PatientMedlData(newPR);
                return Ok(newPR);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatCt")]
        public async Task<IActionResult> UpdatePatCt(PatCtDTO dTO)
        {
            var userD = await _huPCt.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatCt prItem = await _huPCt.PatCt.GetByStringID(dTO.PatCtID);
                if (prItem is null) return NotFound();
                _mPCt.Map(dTO, prItem);
                PatCt upItem = _huPCt.PatCt.Update(prItem);
                await _huPCt.SubmitAsync();
                return Ok(await this.PatientMedlData(upItem));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("StopPatCt")]
        public async Task<IActionResult> StopPatCt(PatCtDTO dTO)
        {
            var userD = await _huPCt.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatCt prItem = await _huPCt.PatCt.GetByStringID(dTO.PatCtID);
                if (prItem is null) return NotFound();
                _mPCt.Map(dTO, prItem);
                PatCt stopPr = await _huPCt.PatCt.RestoreStop(prItem);
                await _huPCt.SubmitAsync();
                return Ok(this.PatientMedlData(stopPr));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelPatCt")]
        public async Task<IActionResult> DelPatCt(string id, string userID)
        {
            var userD = await _huPCt.UserData.GetUserData(userID);
            try
            {
                PatCt pbItem = await _huPCt.PatCt.GetByStringID(id);
                if (pbItem is null) return NoContent();
                await _huPCt.PatCt.Delete(id);
                await _huPCt.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        private async Task<PatCt> PatientMedlData(PatCt pr)
        {
            pr.ParientDataTBL=await _huPCt.PatientData.GetByStringID(pr.PatID);
            pr.BookingTBL= await _huPCt.Booking.GetByStringID(pr.BookID);
            pr.CT_TBL = await _huPCt.CT.GetByStringID(pr.CTID);
            return pr;
        }
    }
}
