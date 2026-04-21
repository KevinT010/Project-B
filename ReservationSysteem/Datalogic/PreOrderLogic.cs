using System.Dynamic;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class PreOrderLogic
{
    // Init _reservationAccess to get access to a new method we are going to use to get all the reservations based on the account 
    private ReservationAccess _reservationAccess = new();
    private readonly GuestAccess _guestAccess = new();

    public List<ReservationModel> GetReservations(long accountId)
    {
        // making list and filling it with reservations by account id
        var Reservations = new List<ReservationModel>();
        Reservations = _reservationAccess.GetByAccountId(accountId);
        return Reservations;
    }

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

    public GuestChoiceModel Insert(long GuestId, long MenuItemId, int Quantity)
    {
        // recieves list with written params changes params too
        return null;
    }

}



