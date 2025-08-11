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
    public class BiometricController : ControllerBase
    {
        readonly IHealthUnits _bioUint; readonly IMapper _bioMap;
        public BiometricController(IHealthUnits healthUnits, IMapper mapper)
        { _bioUint = healthUnits; _bioMap=mapper; }

        [HttpGet("AllBiometrics")]
        public async Task<IActionResult> AllBiometrics() => Ok(await _bioUint.Biometric.GetAll());

        [HttpGet("BiometricsDet")]
        public async Task<IActionResult> BiometricsDet(string id)
        {
            try
            {
                Biometrics item = await _bioUint.Biometric.GetByStringID(id);
                if (item is null) return NotFound();
                return Ok(item);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddBio")]
        public async Task<IActionResult> AddBio(BioDTO dTO)
        {
            try
            {
                Biometrics item = new Biometrics();
                _bioMap.Map(dTO, item);
                Biometrics newSymp = await _bioUint.Biometric.AddItem(item);
                await _bioUint.SubmitAsync();
                return Ok(newSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateBio")]
        public async Task<IActionResult> UpdateBio(BioDTO dTO)
        {
            try
            {
                Biometrics item = await _bioUint.Biometric.GetByStringID(dTO.BioID);
                if (item is null) return NotFound();
                _bioMap.Map(dTO, item);
                Biometrics upSymp = _bioUint.Biometric.Update(item);
                await _bioUint.SubmitAsync();
                return Ok(upSymp);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DelBio")]
        public async Task<IActionResult> DelBio(string id)
        {
            try
            {
                Biometrics item = await _bioUint.Biometric.GetByStringID(id);
                if (item is null) return NotFound();
                await _bioUint.Biometric.Delete(id);
                await _bioUint.SubmitAsync();
                return Ok();
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
    }
}
