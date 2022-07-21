namespace AdventOfCode.Kit.Client.Resources
{
    internal interface IResourceRepository
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
        public Task<CalendarPage> FindCalendarPageByYearAsync(int year);

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
        public Task<DayPage> FindDayPageByYearAndDayAsync(int year, int day);

        /// <summary>
        /// Find a puzzle input for the given year and day index.
        /// </summary>
        /// <param name="year">The year of the puzzle input.</param>
        /// <param name="day">The day index in the puzzle input.</param>
        /// <returns>
        /// The lines of the puzzle input.
        /// </returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
        public Task<string[]> FindPuzzleInputByYearAndDayAsync(int year, int day);

    }
}
