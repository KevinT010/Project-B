using System.Dynamic;
using System.IO.Compression;
using System.Runtime.InteropServices;

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

}



