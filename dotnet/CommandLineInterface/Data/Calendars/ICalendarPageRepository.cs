namespace CommandLineInterface.Data
{
    public interface ICalendarPageRepository
    {
        public Task<CalendarPage> FindByYearAsync(int year);
    }
}
