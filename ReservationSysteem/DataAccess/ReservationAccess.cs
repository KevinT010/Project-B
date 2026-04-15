using Microsoft.Data.Sqlite;
using Dapper;

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
    public void InsertReservation(ReservationModel reservation)
    {
        string query = $@"INSERT INTO {ReservationTable} 
            (AccountId, TableId, DateTime, NumberOfGuests, DurationMinutes) 
            VALUES (@AccountId, @TableId, @DateTime, @NumberOfGuests, @DurationMinutes)";
        _connection.Execute(query, reservation);
    }
}