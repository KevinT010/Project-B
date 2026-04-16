using Microsoft.Data.Sqlite;
using Dapper;

public class MenuAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public void InsertMenuItem(MenuModel menuItem)
    {
        string query = @"INSERT INTO MenuItem (Name, Price, description, foodcategory, allergens) 
                         VALUES (@Name, @Price, @Description, @FoodCategory, @Allergens)";
        
        _connection.Execute(query, menuItem);
    }

    public List<MenuModel> GetAllMenuItems()
    {
        string query = @"
            SELECT MenuItem.*, Menu.MenuName 
            FROM MenuItem
            LEFT JOIN ItemOnMenu ON MenuItem.id = ItemOnMenu.MenuItemId
            LEFT JOIN Menu ON ItemOnMenu.MenuId = Menu.id;";
        
        return _connection.Query<MenuModel>(query).ToList();
    }

    public void LinkItemToMenu(int menuItemId, int menuId)
    {
        string query = "INSERT INTO ItemOnMenu (MenuItemId, MenuId) VALUES (@MenuItemId, @MenuId)";
        
        _connection.Execute(query, new { MenuItemId = menuItemId, MenuId = menuId });
    }
}