using Microsoft.Data.Sqlite;
using Dapper;

public class MenuAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public int InsertMenuItem(MenuModel menuItem, int menuId)
    {
        string query = @"INSERT INTO MenuItem (MenuId, Name, Price, description, foodcategory, allergens) 
                     VALUES (@MenuId, @Name, @Price, @Description, @FoodCategory, @Allergens);
                     SELECT last_insert_rowid();";

        return _connection.ExecuteScalar<int>(query, new
        {
            MenuId = menuId,
            Name = menuItem.Name,
            Price = menuItem.Price,
            Description = menuItem.Description,
            FoodCategory = menuItem.FoodCategory,
            Allergens = menuItem.Allergens
        });
    }

    public List<MenuModel> GetAllMenuItems()
    {
        string query = @"
            SELECT MenuItem.*, Menu.MenuName, Menu.IsActive 
            FROM MenuItem
            LEFT JOIN Menu ON MenuItem.MenuId = Menu.id;";

        return _connection.Query<MenuModel>(query).ToList();
    }

    public List<MenuModel> GetAllMenus()
    {
        string query = "SELECT id as Id, MenuName FROM Menu;";
        return _connection.Query<MenuModel>(query).ToList();
    }

    public void LinkItemToMenu(int menuItemId, int menuId)
    {
        string query = "INSERT INTO ItemOnMenu (MenuItemId, MenuId) VALUES (@MenuItemId, @MenuId)";
        _connection.Execute(query, new { MenuItemId = menuItemId, MenuId = menuId });
    }

    public void CreateMenu(string menuName, bool isActive)
    {
        string query = "INSERT INTO Menu (MenuName, IsActive) VALUES (@MenuName, @IsActive)";
        _connection.Execute(query, new { MenuName = menuName, IsActive = isActive });
    }

    public void UpdateMenu(int menuId, string newMenuName, bool isActive)
    {
        string query = "UPDATE Menu SET MenuName = @MenuName, IsActive = @IsActive WHERE id = @Id";
        _connection.Execute(query, new { MenuName = newMenuName, IsActive = isActive, Id = menuId });
    }

    public void UpdateMenuItem(MenuModel menuItem)
    {
        string query = @"UPDATE MenuItem SET Name = @Name, Price = @Price, description = @Description, 
                         foodcategory = @FoodCategory, allergens = @Allergens WHERE id = @Id";
        _connection.Execute(query, menuItem);
    }

    public bool IsItemInReservation(int menuItemId)
    {
        string query = "SELECT COUNT(1) FROM Guestchoice WHERE MenuItemId = @MenuItemId";
        return _connection.ExecuteScalar<long>(query, new { MenuItemId = menuItemId }) > 0;
    }

    public bool IsMenuInReservation(int menuId)
    {
        string query = @"SELECT COUNT(1) FROM MenuItem mi
                     JOIN Guestchoice gc ON mi.id = gc.MenuItemId
                     WHERE mi.MenuId = @MenuId";
        return _connection.ExecuteScalar<long>(query, new { MenuId = menuId }) > 0;
    }

    public bool DeleteMenuItem(int menuItemId)
    {
        if (IsItemInReservation(menuItemId))
        {
            return false;
        }

        string deleteLinkQuery = "DELETE FROM ItemOnMenu WHERE MenuItemId = @Id";
        _connection.Execute(deleteLinkQuery, new { Id = menuItemId });

        string query = "DELETE FROM MenuItem WHERE id = @Id";
        _connection.Execute(query, new { Id = menuItemId });
        return true;
    }

    public bool DeleteMenu(int menuId)
    {
        if (IsMenuInReservation(menuId))
        {
            return false;
        }

        string deleteLinkQuery = "DELETE FROM ItemOnMenu WHERE MenuId = @Id";
        _connection.Execute(deleteLinkQuery, new { Id = menuId });

        string deleteItemsQuery = "DELETE FROM MenuItem WHERE MenuId = @Id";
        _connection.Execute(deleteItemsQuery, new { Id = menuId });

        string query = "DELETE FROM Menu WHERE id = @Id";
        _connection.Execute(query, new { Id = menuId });
        return true;
    }
}