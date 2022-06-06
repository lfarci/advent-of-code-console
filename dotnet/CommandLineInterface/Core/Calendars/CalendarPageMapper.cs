using AdventOfCode.Console.Web.Resources;

namespace AdventOfCode.Console.Core
{
    internal class CalendarPageMapper
    {
        /// <summary>
        /// Parse the calendar page to a calendar object.
        /// </summary>
        /// <remarks>
        /// The calendar is created with an empty list of days because the
        /// intent is to build the list asynchronously then set its value once
        /// it's ready.
        /// It needs to be done asynchronously because days are made of data
        /// coming from the calendar page and day pages.
        /// </remarks>
        /// <param name="page">This is the page to map.</param>
        /// <returns>A calendar mapped from the given page.</returns>
        public static Calendar ToCalendar(CalendarPage page)
        {
            return new Calendar {
                Year = page.Year,
                Length = page.Days.Count
            };
        }
    }
}
