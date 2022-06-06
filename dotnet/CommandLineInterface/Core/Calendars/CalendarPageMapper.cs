using AdventOfCode.Console.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Console.Core
{
    public class CalendarPageMapper
    {

        public static Calendar ToCalendar(CalendarPage page)
        {
            return new Calendar {
                Year = 0,
                Length = page.Days.Count(),

            };
        }

    }
}
