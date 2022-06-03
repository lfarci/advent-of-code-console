namespace CommandLineInterface.Data
{
    public interface IDayPageRepository
    {
        public Task<DayPage> FindByYearAndDayAsync(int year, int day);
    }
}
