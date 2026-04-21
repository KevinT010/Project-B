using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections;
using SQLitePCL;

public class GuestAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");
    private string ReservationTable = "Guest";
    private string GuestChoiceTable = "GuestChoice";

    public void InsertGuest(GuestModel guest)
    {
        string sql = $"INSERT INTO {ReservationTable} (ReservationId, GuestNumber, Allergens) VALUES (@ReservationId, @GuestNumber, @Allergens)";
        _connection.Execute(sql, guest);
    }

    public GuestModel? GetGuest(long reservationId, int guestNumber)
    {
        string sql = $"SELECT * FROM {ReservationTable} WHERE ReservationId = @ReservationId AND GuestNumber = @GuestNumber";
        return _connection.QueryFirstOrDefault<GuestModel>(sql, new { ReservationId = reservationId, GuestNumber = guestNumber });
    }

    public GuestChoiceModel InsertMenuItems()
    {
        return null;
    }

}