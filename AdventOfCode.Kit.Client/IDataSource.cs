namespace AdventOfCode.Kit.Client.Core
{
    public interface IDataSource
    {
        /// <summary>
        /// Find a calendar for the given year.
        /// </summary>
        /// <param name="year">The year of the calendar.</param>
        /// <returns>
        /// The calendar for the given year with all its days.
        /// </returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
        /// <exception cref="InvalidOperationException">When an internal error occurs.</exception>
        public Task<Calendar> FindCalendarByYearAsync(int year);

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
