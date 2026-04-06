using Microsoft.Data.Sqlite;
using Dapper;

public class TableAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");
    private string Table = "Tables";

    public List<TableModel> GetAllTables()
    {
        string query = $"SELECT * FROM {Table}";
        return _connection.Query<TableModel>(query).ToList();
    }
}