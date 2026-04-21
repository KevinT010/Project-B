using Microsoft.Data.Sqlite;

using Dapper;

public class AccountRegistrationAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Account";

    public void InsertAccount(AccountModel account)
    {
        string query = $"INSERT INTO {Table} (FirstName, LastName, Email, PhoneNumber, Password, AccountLevel) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @AccountLevel, @Points)";
        _connection.Execute(query, account);
    }

    public AccountModel GetByEmail(string email)
    {
        string query = $"SELECT * FROM {Table} WHERE Email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(query, new { Email = email });
    }

    public void UpdatePoints(Int64 accountId, int points)
    {
        string query = $"UPDATE {Table} SET Points = @Points WHERE Id = @Id";
        _connection.Execute(query, new { Points = points, Id = accountId });
    }

    public void UpdateAccount(AccountModel account)
    {
        string query = $"UPDATE {Table} SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PhoneNumber = @PhoneNumber, Password = @Password WHERE Id = @Id";
        _connection.Execute(query, account);
    }

    public void DeleteAccount(int id)
    {
        string query = $"DELETE FROM {Table} WHERE Id = @Id";
        _connection.Execute(query, new { Id = id });
    }
}