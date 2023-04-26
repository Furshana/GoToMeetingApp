using GoToMeetingApp.Handler;
using GoToMeetingApp.Logger;
using GoToMeetingApp.Models;
using GoToMeetingApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GoToMeetingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtmLoginDetailsController : ControllerBase
    {
        private ILoggerService _logger;
        private readonly IJWTManagerRepository _jWTManager;
        private readonly Gotomeeting_dbContext _DbContext;

        public GtmLoginDetailsController(IJWTManagerRepository jWTManager, Gotomeeting_dbContext dbContext, ILoggerService logger)
        {
            this._jWTManager = jWTManager;
            this._DbContext = dbContext;
            _logger = logger;
        }
        [HttpGet("GetLoginUsers")]
        public List<GtmLoginDetails> GetLoginUsers()
        {
            List < GtmLoginDetails> LoginUsers = _DbContext.GtmLoginDetails.ToList();
            return LoginUsers;
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

