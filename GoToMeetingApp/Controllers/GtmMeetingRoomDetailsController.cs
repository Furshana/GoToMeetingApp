using GoToMeetingApp.Handler;
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
    public class GtmMeetingRoomDetailsController : ControllerBase
    {
        private ILoggerService _logger;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly Gotomeeting_dbContext _DbContext;
        public GtmMeetingRoomDetailsController(IJWTManagerRepository jWTManager, Gotomeeting_dbContext dbContext, ILoggerService logger)
        {
            this._jWTManager = jWTManager;
            this._DbContext = dbContext;
            _logger = logger;
        }
       
        [HttpGet("MeetingRoomDetails")]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult GtmMeetingRoomDetails()
        {
            try
            {
                _logger.LogInfo("Displaying all the details of MeetingRoom");
                List<GtmMeetingRoomDetails> gtmMeetingRoomDetails = _DbContext.GtmMeetingRoomDetails.ToList();
                _logger.LogInfo("$Total registered count:{gtmMeetingRoomDetails}");
                return StatusCode(200, gtmMeetingRoomDetails);
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured while displaying the MeetingRoomDetails:{ex.Message}");
                return StatusCode(500, "Internal server error occured while displaying the MeetingRoomDetails.");
                throw;
            }

        }
        [HttpPost]
        public IActionResult AddMeetingDetails(GtmMeetingRoomDetails gtmMeetingRoomDetails)
        {
            try
            {
                _logger.LogInfo("Adding MeetingRoom Details");
                _DbContext.GtmMeetingRoomDetails.Add(gtmMeetingRoomDetails);
                _DbContext.SaveChanges();

                _logger.LogInfo($"Meeting Details added:{gtmMeetingRoomDetails}");
                return StatusCode(201, "Meeting Details Added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while adding the MeetingDetails:{ex.Message}");
                return StatusCode(500, "Internal server error occured while adding the Meeting Details.");
                throw;
            }
        }

    }

}
        

