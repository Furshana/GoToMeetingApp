using GoToMeetingApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GoToMeetingApp.Logger;
using GoToMeetingApp.Repository;

namespace GoToMeetingApp.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class GtmAdminDetailsController : ControllerBase
    {
        private ILoggerService _logger;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly Gotomeeting_dbContext _DbContext;

        public GtmAdminDetailsController(IJWTManagerRepository jWTManager, Gotomeeting_dbContext dbContext, ILoggerService logger)
        {
            this._jWTManager = jWTManager;
            this._DbContext = dbContext;
            _logger = logger;
        }
        [HttpGet( "GetRoomAdmnId")]
        [ServiceFilter(typeof(ActionFilter))]
        public IActionResult GetRoomAdminId()
        {
            try
            {
                _logger.LogInfo("Fetching all the details of Admin");
                List<GtmAdminDetails> gtmAdminDetails = _DbContext.GtmAdminDetails.ToList();
                _logger.LogInfo($"Total no of Admins count:{gtmAdminDetails.Count}");
                return StatusCode(200, gtmAdminDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while fetching the details of Admins:{ex.Message}");
                return StatusCode(500, "Internal server error occured while fetching Admin details.");
                throw;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] Users usersdata)
        {
            var token = _jWTManager.Authenticate(usersdata);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}

        


 
    
    
