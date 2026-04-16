using Microsoft.Data.Sqlite;

using Dapper;

public class AccountRegistrationAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Account";

    public void InsertAccount(AccountModel account)
    {
        string query = $"INSERT INTO {Table} (FirstName, LastName, Email, PhoneNumber, Password, AccountLevel) VALUES (@FirstName, @LastName, @Email, @PhoneNumber, @Password, @AccountLevel)";
        _connection.Execute(query, account);
    }

    public AccountModel GetByEmail(string email)
    {
        string query = $"SELECT * FROM {Table} WHERE Email = @Email";
        return _connection.QueryFirstOrDefault<AccountModel>(query, new { Email = email });
    }

}