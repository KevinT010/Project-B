public class OperatingHourModel
{
    public int Id { get; set; }
    public string Day { get; set; }
    public string OpeningTime { get; set; }
    public string ClosingTime { get; set; }
    public bool IsClosed { get; set; }

    public OperatingHourModel()
    {
    }

    public OperatingHourModel(int id, string day, string openingTime, string closingTime, bool isClosed)
    {
        Id = id;
        Day = day;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        IsClosed = isClosed;
    }
}