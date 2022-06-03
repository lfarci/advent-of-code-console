namespace CommandLineInterface.Data
{
    public interface ICalendarPageRepository
    {
        public Task<Stream> FindCalendarPageAsStreamAsync(int year);
        public Task<CalendarPage> FindByYearAsync(int year);
    }
}
