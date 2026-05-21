public class ReservationLogic
{
    private ReservationAccess _reservationAccess = new();
    private TableLogic _tableLogic = new();

    public List<TableModel> GetAvailableTables(DateTime requestedDateTime, int numberOfGuests, int durationMinutes = 120)
    {
        var allTables = _tableLogic.GetAllTables();
        var availableTables = new List<TableModel>();

        foreach (TableModel table in allTables)
        {
            var overlapping = _reservationAccess.GetOverlappingReservations(table.Id, requestedDateTime, durationMinutes);

            if (table.TableNumber == 15)
            {
                int currentGuests = 0;
                foreach (var restGuest in overlapping)
                {
                    currentGuests += restGuest.NumberOfGuests;
                }
                if (table.Capacity - currentGuests >= numberOfGuests)
                {
                    availableTables.Add(table);
                }
            }
            else
            {
                bool EnoughSeats = table.Capacity >= numberOfGuests;
                bool NoOverlap = overlapping.Count == 0;

                if (EnoughSeats && NoOverlap)
                {
                    availableTables.Add(table);
                }
            }
        }

        List<TableModel> BestTables = new List<TableModel>();
        foreach (TableModel table in availableTables)
        {
            if (table.Capacity == numberOfGuests && table.TableNumber != 15)
            {
                BestTables.Add(table);
            }
        }

        if (BestTables.Count > 0)
        {
            var hibachiTable = availableTables.FirstOrDefault(table => table.TableNumber == 15);
            if (hibachiTable != null && !BestTables.Contains(hibachiTable))
            {
                BestTables.Add(hibachiTable);
            }
            return BestTables;
        }
        else
        {
            return availableTables;
        }
    }

    public bool MakeReservation(Int64 accountId, Int64 tableId, DateTime dateTime, int numberOfGuests, int numberOfKids, int kidsPlayArea, int durationMinutes = 120, bool expired = false, double totalPrice = 0.0)
    {
        var overlapping = _reservationAccess.GetOverlappingReservations(tableId, dateTime, durationMinutes);
        var table = _tableLogic.GetAllTables().FirstOrDefault(table => table.Id == tableId);

        if (table != null && table.TableNumber == 15)
        {
            int currentGuests = 0;
            foreach (var resGuest in overlapping)
            {
                currentGuests += resGuest.NumberOfGuests;
            }

            if (currentGuests + numberOfGuests > table.Capacity)
            {
                return false;
            }
        }
        else if (overlapping.Count > 0)
        {
            return false;
        }

        _reservationAccess.InsertReservation(new ReservationModel(accountId, tableId, dateTime, numberOfGuests, numberOfKids, durationMinutes, expired, totalPrice, kidsPlayArea));
        return true;
    }

    public int GetKidsInPlayArea(DateTime requestedStart, int durationMinutes = 120)
    {
        var overlapping = _reservationAccess.GetOverlappingKidsPlayAreaReservations(requestedStart, durationMinutes);
        int totalKids = 0;

        foreach (var reservation in overlapping)
        {
            totalKids += reservation.KidsPlayArea;
        }

        return totalKids;
    }

    public bool CheckPlayAreaCapacity(DateTime requestedStart, int numberOfKids, int durationMinutes = 120)
    {
        int currentKidsInPlayArea = GetKidsInPlayArea(requestedStart, durationMinutes);
        return currentKidsInPlayArea + numberOfKids <= 10;
    }

    public bool IsExpired(ReservationModel reservation)
    {
        DateTime reservationEnd = reservation.DateTime.AddMinutes(reservation.DurationMinutes);
        return reservationEnd < DateTime.Now;
    }

    public List<ReservationModel> GetActiveReservations(List<ReservationModel> reservations)
    {
        List<ReservationModel> active = [];
        foreach (ReservationModel reservation in reservations)
        {
            if (!IsExpired(reservation))
            {
                active.Add(reservation);
            }
        }
        return active;
    }

    public List<ReservationModel> GetReservationsByAccountId(Int64 accountId)
    {
        return _reservationAccess.GetByAccountId(accountId);
    }

    public List<ReservationModel> GetActiveByAccountId(Int64 accountId)
    {
        return GetActiveReservations(GetReservationsByAccountId(accountId));
    }

    public List<ReservationModel> GetAllReservations()
    {
        return _reservationAccess.GetAllReservations();
    }
}