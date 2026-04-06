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
            bool EnoughSeats = table.Capacity >= numberOfGuests;
            bool NoOverlap = _reservationAccess.GetOverlappingReservations(table.Id, requestedDateTime, durationMinutes).Count == 0;

            if (EnoughSeats && NoOverlap)
            {
                availableTables.Add(table);
            }
        }

        List<TableModel> BestTables = new List<TableModel>();
        foreach (TableModel table in availableTables)
        {
            if (table.Capacity == numberOfGuests)
            {
                BestTables.Add(table);
            }
        }

        if (BestTables.Count > 0)
        {
            return BestTables;
        }
        else
        {
        return availableTables;
        }
    }

    public bool MakeReservation(Int64 accountId, Int64 tableId, DateTime dateTime, int numberOfGuests, int durationMinutes = 120)
    {
        var overlapping = _reservationAccess.GetOverlappingReservations(tableId, dateTime, durationMinutes);
        if (overlapping.Count > 0)
        {
            return false;
        }

        _reservationAccess.InsertReservation(new ReservationModel(accountId, tableId, dateTime, numberOfGuests, durationMinutes));
        return true;
    }
}