using Microsoft.Data.Sqlite;
using Dapper;

public class MenuAccess
{
    private SqliteConnection _connection = new SqliteConnection($"Data Source=DataSources/project.db");
    private string Table = "MenuItem";

    public void InsertMenuItem(MenuModel menuItem)
    {
        string query = $"INSERT INTO {Table} (Name, Price, Description, FoodCategory, Allergens) VALUES (@Name, @Price, @Description, @FoodCategory, @Allergens)";
        _connection.Execute(query, menuItem);
    }

    public List<MenuModel> GetAllMenuItems()
    {
        string query = $"SELECT * FROM {Table};";
        
        return _connection.Query<MenuModel>(query).ToList();
    }
}