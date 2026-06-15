

public class PreOrderLogic
{
    private readonly GuestAccess _guestAccess = new();

    public GuestModel MakeGuest(long reservationId, int guestNumber, string? allergens)
    {
        GuestModel guest = new GuestModel(reservationId, guestNumber, allergens);
        _guestAccess.InsertGuest(guest);
        return guest;
    }

    public GuestModel? GetGuest(long reservationId, int guestNumber)
    {
        return _guestAccess.GetGuest(reservationId, guestNumber);
    }

    public void InsertGuestChoices(List<GuestChoiceModel> allSelectedItems, long reservationId)
    {
        foreach (GuestChoiceModel order in allSelectedItems)
            _guestAccess.InsertGuestChoice(order);
    }

    // PreOrderLogic
    public GuestModel? GetGuestById(long guestId)
    {
        return _guestAccess.GetGuestById(guestId);
    }


}
