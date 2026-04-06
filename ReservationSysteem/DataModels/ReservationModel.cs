public class ReservationModel
{
    public Int64 Id { get; set; }
    public Int64 AccountId { get; set; }
    public Int64 TableId { get; set; }
    public DateTime DateTime { get; set; }
    public int NumberOfGuests { get; set; }
    public int DurationMinutes { get; set; }
    public bool Expired { get; set; }

    public ReservationModel()
    {
    }

    public ReservationModel(Int64 accountid, Int64 tableId, DateTime datetime, int numberOfGuests, int durationMinutes = 120)
    {
        AccountId = accountid;
        TableId = tableId;
        DateTime = datetime;
        NumberOfGuests = numberOfGuests;
        DurationMinutes = durationMinutes;
        Expired = false;
    }
}