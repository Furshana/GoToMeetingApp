using GoToMeetingApp.Logger;
using GoToMeetingApp.Models;
using GoToMeetingApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GoToMeetingApp.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class GtmBookingDetailsController : ControllerBase
    {
        private ILoggerService _logger;
        private readonly Gotomeeting_dbContext _DbContext;
        public GtmBookingDetailsController(Gotomeeting_dbContext dbContext, ILoggerService logger)
        {
            this._DbContext = dbContext;
            _logger = logger;

        }
        [HttpGet("BookingDetails")]
        public IEnumerable<GtmBookingDetails> GetBookingDetails()
        {
            return _DbContext.GtmBookingDetails.ToList();
        }
        [HttpGet("BookingId")]
        public GtmBookingDetails Get(int BookingId)
        {
            return _DbContext.GtmBookingDetails.FirstOrDefault(o => o.BookingId == BookingId);
        }

        public ILoggerService Get_logger()
        {
            return _logger;
        }

        [HttpPost]
        public IActionResult AddBooking(GtmBookingDetails gtmbookingdetails)
        {
            try
            {
                _logger.LogInfo("Adding new booking ID");

                _DbContext.GtmBookingDetails.Add(gtmbookingdetails);
                _DbContext.SaveChanges();

                _logger.LogInfo($"Booking ID added: {gtmbookingdetails}");
                return StatusCode(201, "Booking ID added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while adding the booking ID: {ex.Message}");
                return StatusCode(500, "Internal server error occurred while adding the booking ID.");
                throw;
            }
        }


        [HttpDelete("{RoomId}")]
        public IActionResult Delete(int RoomId)
        {
            GtmBookingDetails id = _DbContext.GtmBookingDetails.Find(RoomId);
            if (id == null)
            {
                return StatusCode(404, "RoomId Not Found");
            }
            else
            {
                _DbContext.GtmBookingDetails.Remove(id);
                _DbContext.SaveChanges();
                return Ok("Deleted Successfully");
            }

        }
    }
}




