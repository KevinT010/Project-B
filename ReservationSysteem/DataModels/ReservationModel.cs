public class ReservationModel
{
    public Int64 Id { get; set; }
    public Int64 AccountId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Int64 TableId { get; set; }
    public DateTime DateTime { get; set; }
    public int NumberOfGuests { get; set; }
    public int NumberOfKids { get; set; }
    public int DurationMinutes { get; set; }
    public bool Expired { get; set; }
    public double? PriceTotal { get; set; }
    public int KidsPlayArea { get; set; }
    public string Notes { get; set; }

    public ReservationModel()
    {
    }

    public ReservationModel(Int64 accountid, Int64 tableId, DateTime datetime, int numberOfGuests, int numberOfKids, int durationMinutes = 120, bool expired = false, double priceTotal = 0.0, int kidsPlayArea = 0, string notes = "")
    {
        AccountId = accountid;
        TableId = tableId;
        DateTime = datetime;
        NumberOfGuests = numberOfGuests;
        NumberOfKids = numberOfKids;
        DurationMinutes = durationMinutes;
        Expired = expired;
        PriceTotal = priceTotal;
        KidsPlayArea = kidsPlayArea;
        Notes = notes;
    }
}