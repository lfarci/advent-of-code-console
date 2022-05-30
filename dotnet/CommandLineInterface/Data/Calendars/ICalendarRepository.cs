namespace CommandLineInterface.Data
{
    public interface ICalendarRepository
    {
        public Task<AdventOfCodeCalendar> FindByYear(int year);
    }
}
