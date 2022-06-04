namespace AdventOfCode.CommandLineInterface.Web
{
    public interface ICalendarPageRepository
    {
        public Task<CalendarPage> FindByYearAsync(int year);
    }
}
