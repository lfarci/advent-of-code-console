namespace AdventOfCode.Kit.Client.Web.Resources
{
    internal interface IDayPageRepository
    {
        /// <summary>
        /// Find a day page for the given year and day index.
        /// </summary>
        /// <param name="year">The year of the calendar.</param>
        /// <param name="day">The day index in the calendar.</param>
        /// <returns>
        /// The data that was parsed from the day page.
        /// </returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
        /// <exception cref="InvalidOperationException">When the response has an unexpected format.</exception>
        public Task<DayPage> FindByYearAndDayAsync(int year, int day);
    }
}
