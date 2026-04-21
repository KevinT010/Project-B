using System.Dynamic;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class PreOrderLogic
{
    // Init _reservationAccess to get access to a new method we are going to use to get all the reservations based on the account 
    private ReservationAccess _reservationAccess = new();
    private readonly GuestAccess _guestAccess = new();

    // heb een method nodig die eerst een guest aan maakt
    public GuestModel MakeGuest(long reservationId, int guestNumber, string? allergens)
    {
        GuestModel guest = new GuestModel(reservationId, guestNumber, allergens);

        GuestAccess access = new GuestAccess();
        access.InsertGuest(guest);

        return guest;
    }

    // have to check if selected user allready has filled in there allergens
    public GuestModel? GetGuest(long reservationId, int guestNumber)
    {
        return _guestAccess.GetGuest(reservationId, guestNumber);
    }

    public void InsertGuestChoices(List<(int GuestNumber, MenuModel Item, int Quantity)> allSelectedItems, long reservationId)
    {
        foreach (var order in allSelectedItems)
        {
            GuestModel? guest = _guestAccess.GetGuest(reservationId, order.GuestNumber);
            if (guest == null)
                continue;

            GuestChoiceModel choice = new GuestChoiceModel(order.Item.Id, guest.Id, order.Quantity);
            _guestAccess.InsertGuestChoice(choice);
        }
    }

}



