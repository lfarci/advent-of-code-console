namespace AdventOfCode.Console.Web
{
    public interface ICalendarPageRepository
    {
        /// <summary>
        /// Find a calendar page for the given year.
        /// </summary>
        /// <param name="year">The year of the calendar.</param>
        /// <returns>
        /// The data that was parsed from the calendar page.
        /// </returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
        /// <exception cref="InvalidOperationException">When the response has an unexpected format.</exception>
        public Task<CalendarPage> FindByYearAsync(int year);
    }
}
