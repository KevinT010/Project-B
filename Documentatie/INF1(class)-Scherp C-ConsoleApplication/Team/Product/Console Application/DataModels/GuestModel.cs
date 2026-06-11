public class GuestModel
{
    public Int64 Id { get; set; }
    public Int64 ReservationId { get; set; }
    public int GuestNumber { get; set; }
    public string Allergens { get; set; }

    public GuestModel()
    {
    }

    public GuestModel(Int64 ReservationId, int GuestNumber, string? Allergens)
    {
        this.ReservationId = ReservationId;
        this.GuestNumber = GuestNumber;
        this.Allergens = Allergens;
    }
}