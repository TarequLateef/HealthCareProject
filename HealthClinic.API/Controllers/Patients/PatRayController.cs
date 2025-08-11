using AutoMapper;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Identity.Client;

namespace HealthClinic.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatRayController : ControllerBase
    {
        readonly IHealthUnits _huPR; readonly IMapper _mPR;
        public PatRayController(IHealthUnits huPR, IMapper mPR)
        {
            _huPR=huPR;
            _mPR=mPR;
        }

        [HttpGet("AllPatRays")]
        public async Task<IActionResult> AllPatRays(bool? aval, string? patRayID, string userID)
        {
            var userD = await _huPR.UserData.GetUserData(userID);
            try
            {
                var prList = aval.HasValue ? aval.Value ?
                    await _huPR.PatRay.AvailableListAsync()
                    : await _huPR.PatRay.BannedListAsync()
                    : await _huPR.PatRay.Find(new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Rays });
                prList = prList.Where(pr => pr.CompID==userD.CompID).ToList();
                if (!string.IsNullOrEmpty(patRayID))
                    prList=prList.Where(pr => pr.PatientID==patRayID).ToList();
                return Ok(prList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PatRayDet")]
        public async Task<IActionResult> PatRayDet(string id, string userID)
        {
            var userD = await _huPR.UserData.GetUserData(userID);
            try
            {
                var prItem = await _huPR.PatRay.GetByStringID(id);
                if (prItem is null) return NotFound();
                return Ok(await this.PatientRayData(prItem));
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpPost("AddPatRay")]
        public async Task<IActionResult> AddPatRay(PatRayDTO dTO)
        {
                var userD = await _huPR.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatientRays prItem = new PatientRays();
                _mPR.Map(dTO, prItem);
                var newPR = await _huPR.PatRay.AddItem(prItem);
                await _huPR.SubmitAsync();
                newPR = await this.PatientRayData(newPR);
                return Ok(newPR);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatRay")]
        public async Task<IActionResult> UpdatePatRay(PatRayDTO dTO)
        {
            var userD = await _huPR.UserData.GetUserData(dTO.UserLogID);
            try
            {
                var prItem = await _huPR.PatRay.GetByStringID(dTO.PRID);
                if (prItem is null) return NotFound();
                _mPR.Map(dTO, prItem);
                var upItem = _huPR.PatRay.Update(prItem);
                await _huPR.SubmitAsync();
                upItem = await this.PatientRayData(upItem);
                return Ok(upItem);
            }
            catch(Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("StopPatRay")]
        public async Task<IActionResult> StopPatRay(PatRayDTO dTO)
        {
            var userD = await _huPR.UserData.GetUserData(dTO.UserLogID);
            try
            {
                var prItem = await _huPR.PatRay.GetByStringID(dTO.PRID);
                if (prItem is null) return NotFound();
                _mPR.Map(dTO, prItem);
                var stopPr = await _huPR.PatRay.RestoreStop(prItem);
                await _huPR.SubmitAsync();
                return Ok(this.PatientRayData(stopPr));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelPatRay")]
        public async Task<IActionResult> DelPatRay(string id, string userID)
        {
            var userD = await _huPR.UserData.GetUserData(userID);
            try
            {
                var pbItem = await _huPR.PatRay.GetByStringID(id);
                if (pbItem is null) return NoContent();
                await _huPR.PatRay.Delete(id);
                await _huPR.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        private async Task<PatientRays> PatientRayData(PatientRays pr) {
            pr.ParientDataTBL=await _huPR.PatientData.GetByStringID(pr.PatientID);
            pr.BookingTBL= await _huPR.Booking.GetByStringID(pr.BookingID);
            pr.RaysTBL = await _huPR.Rays.GetByStringID(pr.RayID);
            return pr;
        }
    }
}
