using Microsoft.Data.Sqlite;
using Dapper;

public class GuestAccess
{
    private string _connectionString = "Data Source=DataSources/project.db";
    private string ReservationTable = "Guest";
    private string GuestChoiceTable = "GuestChoice";

    public void InsertGuest(GuestModel guest)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"INSERT INTO {ReservationTable} (ReservationId, GuestNumber, Allergens) VALUES (@ReservationId, @GuestNumber, @Allergens)";
        connection.Execute(sql, guest);
    }

    public GuestModel? GetGuest(long reservationId, int guestNumber)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"SELECT * FROM {ReservationTable} WHERE ReservationId = @ReservationId AND GuestNumber = @GuestNumber";
        return connection.QueryFirstOrDefault<GuestModel>(sql, new { ReservationId = reservationId, GuestNumber = guestNumber });
    }

    public void InsertGuestChoice(GuestChoiceModel guestChoice)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"INSERT INTO {GuestChoiceTable} (GuestId, MenuItemId, Quantity) VALUES (@GuestId, @MenuItemId, @Quantity)";
        connection.Execute(sql, guestChoice);
    }
}