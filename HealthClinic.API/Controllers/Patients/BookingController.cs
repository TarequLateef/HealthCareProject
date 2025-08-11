using AutoMapper;
using GeneralMotabea.Core.DTOs;
using GeneralMotabea.Core.General.DbStructs;
using Health.Motabea.Core;
using Health.Motabea.Core.DTOs.Patients;
using Health.Motabea.Core.Models.Patients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HealthClinic.API.Controllers.Patients
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        readonly IHealthUnits _hu; readonly IMapper _huMap; 
        
        public BookingController(IHealthUnits hu, IMapper mapper)
        { _hu= hu; _huMap = mapper; }

        [HttpGet("AllBooking")]
        public async Task<IActionResult> AllBooking(bool? aval, DateTime? BDate, string userID)
        {
            try
            {
                AutherData user = await _hu.UserData.GetUserData(userID);
                var bookList = aval.HasValue ? aval.Value ?
                    await _hu.Booking.AvailableListAsync()
                    : await _hu.Booking.BannedListAsync()
                    : await _hu.Booking.Find(new[] { PatientTab.BaseData });
                if (BDate.HasValue)
                    bookList=bookList.Where(bl => bl.StartDate.Date==BDate.Value.Date).ToList();
                return Ok(bookList.Where(b=>b.CompID==user.CompID));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("BookData")]
        public async Task<IActionResult> BookData(string id,string userID)
        {
            try
            {
                AutherData user = await _hu.UserData.GetUserData(userID);
                var bItem = await _hu.Booking.GetByStringID(id);
                Booking booking = await this.BookingData(bItem);
                return Ok(booking);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(BookDTO dTO)
        {
            try
            {
                Booking booking = new Booking();
                _huMap.Map(dTO, booking);
                if (await _hu.Booking.Repeated(booking))
                    return BadRequest();
                var bItem = await _hu.Booking.AddItem(booking);
                await _hu.SubmitAsync();
                bItem = await this.BookingData(bItem);
                /*await _hubContex.Clients.All.SendAsync(HubVars.takeBook, bItem.PatientBaseTBL.PatientName);*/
                return Ok(bItem);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(BookDTO dTO)
        {
            try
            {
                var bItem = await _hu.Booking.GetByStringID(dTO.BookID);
                _huMap.Map(dTO, bItem);
                var upB = _hu.Booking.Update(bItem);
                await _hu.SubmitAsync();
                upB=await this.BookingData(upB);
                return Ok(upB);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(string id)
        {
            var bItem = await _hu.Booking.GetByStringID(id);
            _hu.Booking.Delete(bItem);
            await _hu.SubmitAsync();
            return Ok();
        }

        [HttpPut("StopBook")]
        public async Task<IActionResult> StopBook(BookDTO bTO)
        {
            try
            {
                var bItem = await _hu.Booking.GetByStringID(bTO.BookID);
                _huMap.Map(bTO, bItem);
                var RestBook = await _hu.Booking.RestoreStop(bItem);
                await _hu.SubmitAsync();
                RestBook = await this.BookingData(RestBook);
                return Ok(RestBook);
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }
        async Task<Booking> BookingData(Booking booking)
        {
            booking.PatientBaseTBL = await _hu.PatientBaseData.GetByStringID(booking.PatientID);
            return booking;
        }
    }
}
