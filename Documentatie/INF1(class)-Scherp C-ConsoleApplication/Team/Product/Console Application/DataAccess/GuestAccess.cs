using Microsoft.Data.Sqlite;
using Dapper;

public class GuestAccess
{
    private string _connectionString = "Data Source=DataSources/project.db";
    private string GuestTable = "Guest";
    private string GuestChoiceTable = "GuestChoice";

    public void InsertGuest(GuestModel guest)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"INSERT INTO {GuestTable} (ReservationId, GuestNumber, Allergens) VALUES (@ReservationId, @GuestNumber, @Allergens)";
        connection.Execute(sql, guest);
    }

    public GuestModel? GetGuest(long reservationId, int guestNumber)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"SELECT * FROM {GuestTable} WHERE ReservationId = @ReservationId AND GuestNumber = @GuestNumber";
        return connection.QueryFirstOrDefault<GuestModel>(sql, new { ReservationId = reservationId, GuestNumber = guestNumber });
    }

    public GuestModel? GetGuestById(long guestId)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"SELECT * FROM {GuestTable} WHERE Id = @Id";
        return connection.QueryFirstOrDefault<GuestModel>(sql, new { Id = guestId });
    }

    public void InsertGuestChoice(GuestChoiceModel guestChoice)
    {
        using SqliteConnection connection = new SqliteConnection(_connectionString);
        string sql = $"INSERT INTO {GuestChoiceTable} (GuestId, MenuItemId, Quantity) VALUES (@GuestId, @MenuItemId, @Quantity)";
        connection.Execute(sql, guestChoice);
    }
}

