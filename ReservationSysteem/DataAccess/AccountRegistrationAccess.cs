using Microsoft.Data.Sqlite;

using Dapper;

public class AccountRegistrationAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");

    private string Table = "Account";

    public void InsertAccount(AccountModel account)
    {
        string query = $"INSERT INTO {Table} (FullName, Email, PhoneNumber, Password) VALUES (@FullName, @Email, @PhoneNumber, @Password)";
        _connection.Execute(query, account);
    }

}