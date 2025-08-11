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
    public class MedicineController : ControllerBase
    {
        readonly IHealthUnits _medUint; readonly IMapper _medMap;
        public MedicineController(IHealthUnits healthUnits, IMapper mapper)
        { _medUint = healthUnits; _medMap=mapper; }

        [HttpGet("AllMedicine")]
        public async Task<IActionResult> AllMedicine() => Ok(await _medUint.Medicine.GetAll());

        [HttpGet("MedicineDet")]
        public async Task<IActionResult> MedicineDet(string id)
        {
            try
            {
                Medicine item = await _medUint.Medicine.GetByStringID(id);
                if (item is null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddMedicine")]
        public async Task<IActionResult> AddMedicine(MedicinDTO dTO)
        {
            try
            {
                Medicine item = new Medicine();
                _medMap.Map(dTO, item);
                Medicine newSymp = await _medUint.Medicine.AddItem(item);
                await _medUint.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateMedicine")]
        public async Task<IActionResult> UpdateMedicine(MedicinDTO dTO)
        {
            try
            {
                Medicine item = await _medUint.Medicine.GetByStringID(dTO.MedID);
                if (item is null) return NotFound();
                _medMap.Map(dTO, item);
                Medicine upSymp = _medUint.Medicine.Update(item);
                await _medUint.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelMedicine")]
        public async Task<IActionResult> DelMedicine(string id)
        {
            try
            {
                Medicine item = await _medUint.Medicine.GetByStringID(id);
                if (item is null) return NotFound();
                await _medUint.Medicine.Delete(id);
                await _medUint.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
