using Microsoft.Data.Sqlite;
using Dapper;

public class OperatingHourAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public List<OperatingHourModel> GetAll()
    {
        string query = "SELECT * FROM OperatingHour";
        return _connection.Query<OperatingHourModel>(query).ToList();
    }

    public void Update(OperatingHourModel hours)
    {
        string query = "UPDATE OperatingHour SET OpeningTime = @OpeningTime, ClosingTime = @ClosingTime, IsClosed = @IsClosed WHERE Id = @Id";
        _connection.Execute(query, hours);
    }
}