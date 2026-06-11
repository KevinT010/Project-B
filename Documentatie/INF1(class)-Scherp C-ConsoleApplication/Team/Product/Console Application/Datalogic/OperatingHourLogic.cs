public class OperatingHourLogic
{
    private OperatingHourAccess _access = new();
    private List<OperatingHourModel> _operatingHour;

    public OperatingHourLogic()
    {
        _operatingHour = _access.GetAll();
    }

    public List<OperatingHourModel> GetHours()
    {
        _operatingHour = _access.GetAll();
        return _operatingHour ?? new List<OperatingHourModel>();
    }

    public OperatingHourModel GetByDay(string day)
    {
        if (_operatingHour == null || day == null)
        {
            return null;
        }

        string targetDay = day.ToLower();

        foreach (OperatingHourModel hour in _operatingHour)
        {
            if (hour.Day != null && hour.Day.ToLower() == targetDay)
            {
                return hour;
            }
        }

        return null;
    }

    public void UpdateOperatingHours(OperatingHourModel hours)
    {
        _access.Update(hours);
    }

    public bool IsOpen(DateTime dateTime)
    {
        var dayInfo = GetByDay(dateTime.DayOfWeek.ToString());
        if (dayInfo == null || dayInfo.IsClosed) return false;

        TimeSpan current = dateTime.TimeOfDay;
        TimeSpan open = TimeSpan.Parse(dayInfo.OpeningTime);
        TimeSpan close = TimeSpan.Parse(dayInfo.ClosingTime);

        return current >= open && current <= close;
    }
}