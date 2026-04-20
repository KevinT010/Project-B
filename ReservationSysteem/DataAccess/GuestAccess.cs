using Microsoft.Data.Sqlite;
using Dapper;
using System.Collections;
using SQLitePCL;

public class GuestModel
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");
    private string ReservationTable = "Guest";

    public void InsertUser(GuestModel guest)
    {
        return;
    }

}