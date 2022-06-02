namespace CommandLineInterface.Data
{
    public interface ICalendarPageRepository
    {
        public Task<CalendarPage> FindByYear(int year);
    }
}
