namespace AdventOfCode.Console.Core
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
    }
}
