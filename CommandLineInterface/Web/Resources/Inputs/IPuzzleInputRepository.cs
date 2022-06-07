namespace AdventOfCode.Console.Web.Resources
{
    internal interface IPuzzleInputRepository
    {
        /// <summary>
        /// Find a puzzle input for the given year and day index.
        /// </summary>
        /// <param name="year">The year of the puzzle input.</param>
        /// <param name="day">The day index in the puzzle input.</param>
        /// <returns>
        /// The lines of the puzzle input.
        /// </returns>
        /// <exception cref="IOException">Thrown when an I/O error occurs.</exception>
        public Task<string[]> FindByYearAndDayAsync(int year, int day);
    }
}
