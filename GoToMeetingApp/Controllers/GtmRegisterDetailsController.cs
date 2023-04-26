using GoToMeetingApp.Logger;
using GoToMeetingApp.Models;
using GoToMeetingApp.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace GoToMeetingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtmRegisterDetailsController : ControllerBase
    {
        private ILoggerService _logger;
        private IJWTManagerRepository _jWTManager;
        private readonly Gotomeeting_dbContext _Context;
        public GtmRegisterDetailsController(IJWTManagerRepository jWTManager, Gotomeeting_dbContext dbContext, ILoggerService logger)
        {
           this._jWTManager = jWTManager;
           this._Context = dbContext;
            _logger = logger; 
        }

        [HttpGet("GetMember")]
        public List<GtmRegisterDetails> GetMember()
        {
            List<GtmRegisterDetails> members = _Context.GtmRegisterDetails.ToList();
            return members;
        }

        [HttpPost("Newuser")]
        public IActionResult Post([FromBody] GtmRegisterDetails NewUser)
        {
            _Context.GtmRegisterDetails.Add(NewUser);
            _Context.SaveChanges();
            return Created("Registering Details Added", NewUser);
        }

    }
}

        
