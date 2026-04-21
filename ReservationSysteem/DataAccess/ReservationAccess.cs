using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections;
using SQLitePCL;

public class ReservationAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");
    private string ReservationTable = "Reservation";

    public List<ReservationModel> GetOverlappingReservations(Int64 tableId, DateTime requestedStart, int durationMinutes)
    {
        string query = $"SELECT * FROM {ReservationTable} WHERE TableId = @TableId";
        var allReservations = _connection.Query<ReservationModel>(query, new { TableId = tableId }).ToList();

        DateTime requestedEnd = requestedStart.AddMinutes(durationMinutes);
        var overlappingReservations = new List<ReservationModel>();

        foreach (ReservationModel reservation in allReservations)
        {
            DateTime existingStart = reservation.DateTime;
            DateTime existingEnd = reservation.DateTime.AddMinutes(reservation.DurationMinutes);

            if (requestedStart < existingEnd && requestedEnd > existingStart)
            {
                overlappingReservations.Add(reservation);
            }
        }

        return overlappingReservations;
    }

    public List<ReservationModel> GetByAccountId(long accountId)
    {
        // List with Reservations based on account id
        string query = $"SELECT * FROM {ReservationTable} WHERE AccountId = @AccountId";
        // opens, executes, read etc
        var Reservations = _connection.Query<ReservationModel>(query, new { AccountId = accountId }).ToList();

        return Reservations;
    }

    public List<ReservationModel> GetAllReservations()
    {
        // List with all Reservations
        string query = $"SELECT * FROM {ReservationTable}";
        // opens, executes, read etc
        var Reservations = _connection.Query<ReservationModel>(query, new()).ToList();

        return Reservations;
    }

    public void InsertReservation(ReservationModel reservation)
    {
        string query = $@"INSERT INTO {ReservationTable} 
            (AccountId, TableId, DateTime, NumberOfGuests, NumberOfKids, DurationMinutes, Expired, PriceTotal) 
            VALUES (@AccountId, @TableId, @DateTime, @NumberOfGuests, @NumberOfKids, @DurationMinutes, @Expired, @PriceTotal)";
        _connection.Execute(query, reservation);
    }
}