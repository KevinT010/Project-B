using System.Dynamic;
using System.IO.Compression;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

public class PreOrderLogic
{
    // Init _reservationAccess to get access to a new method we are going to use to get all the reservations based on the account 
    private ReservationAccess _reservationAccess = new();

    public List<ReservationModel> GetReservations(long accountId)
    {
        // making list and filling it with reservations by account id
        var Reservations = new List<ReservationModel>();
        Reservations = _reservationAccess.GetByAccountId(accountId);
        return Reservations;
    }

    // heb een method nodig die eerst een guest aan maakt
    public GuestModel MakeGuest(int ReservationId, int GuestNumber, string Allergens)
    {
        // Method params: int ReservationId, int GuestNumber, string Allergens
        // omdat guest choice een guest id nodig heeft en guest moet dan bestaan    


        return null;
    }

    // have to check if selected user allready has filled in there allergens


}



