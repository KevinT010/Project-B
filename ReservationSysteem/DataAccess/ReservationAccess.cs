using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections;
using SQLitePCL;

public class ReservationAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");
    private string ReservationTable = "Reservation";
    private string GuestTable = "Guest";
    private string GuestChoiceTable = "GuestChoice";

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

    public List<ReservationModel> GetActiveByAccountId(long accountId)
    {
        // List with Reservations based on account id
        string query = $"SELECT * FROM {ReservationTable} WHERE AccountId = @AccountId";
        // opens, executes, read etc
        var Reservations = _connection.Query<ReservationModel>(query, new { AccountId = accountId }).ToList();

        return Reservations;
    }

    public List<ReservationModel> GetAllReservations()
    {
        string query = $@"SELECT res.*, acc.FirstName, acc.LastName 
                        FROM {ReservationTable} res
                        LEFT JOIN Account acc ON res.AccountId = acc.Id";
        return _connection.Query<ReservationModel>(query).ToList();
    }

    public void InsertReservation(ReservationModel reservation)
    {
        string query = $@"INSERT INTO {ReservationTable} 
            (AccountId, TableId, DateTime, NumberOfGuests, NumberOfKids, DurationMinutes, Expired, PriceTotal) 
            VALUES (@AccountId, @TableId, @DateTime, @NumberOfGuests, @NumberOfKids, @DurationMinutes, @Expired, @PriceTotal)";
        _connection.Execute(query, reservation);
    }

    public void DeleteReservationsByUser(long userId)
    {
        string deleteGuestChoices = $"DELETE FROM {GuestChoiceTable} WHERE GuestId IN (SELECT guest.Id FROM {GuestTable} guest JOIN {ReservationTable} reservation ON guest.ReservationId = reservation.Id WHERE reservation.AccountId = @AccountId);";
        _connection.Execute(deleteGuestChoices, new { AccountId = userId });

        string deleteGuests = $"DELETE FROM {GuestTable} WHERE ReservationId IN (SELECT Id FROM {ReservationTable} WHERE AccountId = @AccountId);";
        _connection.Execute(deleteGuests, new { AccountId = userId });

        string deleteReservations = $"DELETE FROM {ReservationTable} WHERE AccountId = @AccountId";
        _connection.Execute(deleteReservations, new { AccountId = userId });
    }
}