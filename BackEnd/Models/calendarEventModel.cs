using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace IntakeSysteemBack.Models
{
    public class CalendarEvent
    {
        public string Date { get; set; }

        public string startTime {get; set;}

        public string location { get; set; }

        public string CC { get; set; }

        public int Color { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Token { get; set; }

    }
}
