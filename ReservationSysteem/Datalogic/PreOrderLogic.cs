using System.Dynamic;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class PreOrderLogic
{
    private readonly GuestAccess _guestAccess = new();

    public GuestModel MakeGuest(int reservationId, int guestNumber, string? allergens)
    {
        GuestModel guest = new GuestModel(reservationId, guestNumber, allergens);
        _guestAccess.InsertGuest(guest);
        return guest;
    }

    public GuestModel? GetGuest(int reservationId, int guestNumber)
    {
        return _guestAccess.GetGuest(reservationId, guestNumber);
    }

    public void InsertGuestChoices(List<GuestChoiceModel> allSelectedItems, int reservationId)
    {
        foreach (GuestChoiceModel order in allSelectedItems)
            _guestAccess.InsertGuestChoice(order);
    }

    // PreOrderLogic
    public GuestModel? GetGuestById(int guestId)
    {
        return _guestAccess.GetGuestById(guestId);
    }


}
