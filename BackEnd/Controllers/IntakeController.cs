using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.Logic;
using IntakeSysteemBack.Models;

namespace IntakeSysteemBack.Controllers
{
    [Route("api/Intake")]
    [ApiController]
    public class IntakeController : Controller
    {
        private readonly CalendarLogic _calendarLogic;
        private readonly JWT _jwt;

        public IntakeController(CalendarLogic calendarlogic, JWT jwt)
        {
            _calendarLogic = calendarlogic;
            _jwt = jwt;
        }

        [HttpPost("CalendarEvent"), DisableRequestSizeLimit]
        public IActionResult newCalendarEvent([FromBody] CalendarEvent CEM)
        {
            try
            {
                if (_jwt.Validate(CEM.Token))
                {
                    _calendarLogic.createCalendarEvent(CEM);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
                
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
