using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeSysteemBack.Models;
using IntakeSysteemBack.DAL;
using IntakeSysteemBack.Interfaces;

namespace IntakeSysteemBack.Logic
{
    public class CalendarLogic
    {
        readonly ICalendar _Icalendar;

        //REMEMBER TO ADD STARTUP IF YOU ARE COPYING ME!!!

        public CalendarLogic(ICalendar ical)
        {
            _Icalendar = ical;
        }

        public void createCalendarEvent(CalendarEvent EventFromFront)
        {
            _Icalendar.createCalendarEvent(EventFromFront);
        }

    }
}
