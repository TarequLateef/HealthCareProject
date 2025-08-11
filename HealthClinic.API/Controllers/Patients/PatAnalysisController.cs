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
    public class PatAnalysisController : ControllerBase
    {
        readonly IHealthUnits _huPA; readonly IMapper _mPA;
        public PatAnalysisController(IHealthUnits huPA, IMapper mPA)
        { _huPA=huPA; _mPA=mPA; }

        [HttpGet("AllPatAnalysis")]
        public async Task<IActionResult> AllPatAnalysis(bool? aval,string? patAnalID, string userID)
        {
            var userD = await _huPA.UserData.GetUserData(userID);
            try
            {
                IList<PatAnalysis> prList = aval.HasValue ? aval.Value ?
                    await _huPA.PatAnalysis.AvailableListAsync()
                    : await _huPA.PatAnalysis.BannedListAsync()
                    : await _huPA.PatAnalysis.Find(new[] { PatientTab.Patient, PatientTab.Booking, ServTab.Analysis });
                prList = prList.Where(pr => pr.CompID==userD.CompID).ToList();
                if (!string.IsNullOrEmpty(patAnalID))
                    prList=prList.Where(pr => pr.PatID==patAnalID).ToList();        
                return Ok(prList);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("PartAnalDet")]
        public async Task<IActionResult> PartAnalDet(string id, string userID)
        {
            var userD = await _huPA.UserData.GetUserData(userID);
            try
            {
                PatAnalysis prItem = await _huPA.PatAnalysis.GetByStringID(id);
                if (prItem is null) return NotFound();
                return Ok(await this.PatientAnalData(prItem));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        [HttpPost("AddPatAnal")]
        public async Task<IActionResult> AddPatAnal(PatAnalysDTO dTO)
        {
            var userD = await _huPA.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatAnalysis prItem = new PatAnalysis();
                _mPA.Map(dTO, prItem);
                PatAnalysis newPR = await _huPA.PatAnalysis.AddItem(prItem);
                await _huPA.SubmitAsync();
                return Ok(await this.PatientAnalData(newPR));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdatePatAnal")]
        public async Task<IActionResult> UpdatePatAnal(PatAnalysDTO dTO)
        {
            var userD = await _huPA.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatAnalysis prItem = await _huPA.PatAnalysis.GetByStringID(dTO.PatAnalID);
                if (prItem is null) return NotFound();
                _mPA.Map(dTO, prItem);
                PatAnalysis upItem = _huPA.PatAnalysis.Update(prItem);
                await _huPA.SubmitAsync();
                return Ok(await this.PatientAnalData(upItem));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("StopPatAnal")]
        public async Task<IActionResult> StopPatAnal(PatAnalysDTO dTO)
        {
            var userD = await _huPA.UserData.GetUserData(dTO.UserLogID);
            try
            {
                PatAnalysis prItem = await _huPA.PatAnalysis.GetByStringID(dTO.PatAnalID);
                if (prItem is null) return NotFound();
                _mPA.Map(dTO, prItem);
                PatAnalysis stopPr = await _huPA.PatAnalysis.RestoreStop(prItem);
                await _huPA.SubmitAsync();
                return Ok(this.PatientAnalData(stopPr));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        [HttpDelete("DelPatAnal")]
        public async Task<IActionResult> DelPatAnal(string id, string userID)
        {
            var userD = await _huPA.UserData.GetUserData(userID);
            try
            {
                PatAnalysis pbItem = await _huPA.PatAnalysis.GetByStringID(id);
                if (pbItem is null) return NoContent();
                await _huPA.PatAnalysis.Delete(id);
                await _huPA.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }


        private async Task<PatAnalysis> PatientAnalData(PatAnalysis pr)
        {
            pr.ParientDataTBL=await _huPA.PatientData.GetByStringID(pr.PatID);
            pr.BookingTBL= await _huPA.Booking.GetByStringID(pr.BookID);
            pr.AnalysisTBL = await _huPA.Analysis.GetByStringID(pr.AnalysisID);
            return pr;
        }
    }
}
