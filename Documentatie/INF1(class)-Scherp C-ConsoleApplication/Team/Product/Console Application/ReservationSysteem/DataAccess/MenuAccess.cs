using Microsoft.Data.Sqlite;
using Dapper;

public class MenuAccess
{
    private SqliteConnection _connection = new SqliteConnection("Data Source=DataSources/project.db");

    public int InsertMenuItem(MenuModel menuItem, int menuId)
    {
        string query = @"INSERT INTO MenuItem (MenuId, Name, Price, description, foodcategory, allergenid) 
                     VALUES (@MenuId, @Name, @Price, @Description, @FoodCategory, @AllergenId);
                     SELECT last_insert_rowid();";

        return _connection.ExecuteScalar<int>(query, new
        {
            MenuId = menuId,
            Name = menuItem.Name,
            Price = menuItem.Price,
            Description = menuItem.Description,
            FoodCategory = menuItem.FoodCategory,
            AllergenId = menuItem.AllergenId
        });
    }

    public List<MenuModel> GetAllMenuItems()
    {
        string query = @"
            SELECT 
                MenuItem.id,
                MenuItem.MenuId,
                MenuItem.Name,
                MenuItem.Price,
                MenuItem.description,
                MenuItem.foodcategory,
                Menu.MenuName, 
                Menu.IsActive, 
                IFNULL(GROUP_CONCAT(Allergen.Name, ', '), 'None') AS AllergenName 
            FROM MenuItem
            LEFT JOIN Menu ON MenuItem.MenuId = Menu.id
            LEFT JOIN AllergenOnMenu ON MenuItem.id = AllergenOnMenu.NameId
            LEFT JOIN Allergen ON AllergenOnMenu.AllergenId = Allergen.id
            GROUP BY 
                MenuItem.id, MenuItem.MenuId, MenuItem.Name, MenuItem.Price, 
                MenuItem.description, MenuItem.foodcategory, Menu.MenuName, Menu.IsActive;";

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
                         foodcategory = @FoodCategory, allergenid = @AllergenId WHERE id = @Id";
        _connection.Execute(query, menuItem);
    }

    public bool IsItemInReservation(int menuItemId)
    {
        string choiceQuery = "SELECT GuestId FROM GuestChoice WHERE MenuItemId = @MenuItemId";
        var guestIds = _connection.Query<long>(choiceQuery, new { MenuItemId = menuItemId }).ToList();

        foreach (long guestId in guestIds)
        {
            string guestQuery = "SELECT ReservationId FROM Guest WHERE Id = @Id";
            long reservationId = _connection.ExecuteScalar<long>(guestQuery, new { Id = guestId });

            string reservationQuery = "SELECT DateTime FROM Reservation WHERE Id = @Id";
            DateTime reservationDate = _connection.ExecuteScalar<DateTime>(reservationQuery, new { Id = reservationId });

            if (reservationDate >= DateTime.Now)
            {
                return true;
            }
        }

        return false;
    }

    public bool IsMenuInReservation(int menuId)
    {
        string itemQuery = "SELECT id FROM MenuItem WHERE MenuId = @MenuId";
        var itemIds = _connection.Query<int>(itemQuery, new { MenuId = menuId }).ToList();

        foreach (int itemId in itemIds)
        {
            if (IsItemInReservation(itemId))
            {
                return true;
            }
        }

        return false;
    }

public bool DeleteMenuItem(int menuItemId)
    {
        if (IsItemInReservation(menuItemId))
        {
            return false;
        }

        string deleteAllergensQuery = "DELETE FROM AllergenOnMenu WHERE NameId = @Id";
        _connection.Execute(deleteAllergensQuery, new { Id = menuItemId });

        string deleteLinkQuery = "DELETE FROM ItemOnMenu WHERE MenuItemId = @Id";
        _connection.Execute(deleteLinkQuery, new { Id = menuItemId });

        string deleteChoicesQuery = "DELETE FROM GuestChoice WHERE MenuItemId = @Id";
        _connection.Execute(deleteChoicesQuery, new { Id = menuItemId });

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

        string itemQuery = "SELECT id FROM MenuItem WHERE MenuId = @MenuId";
        var itemIds = _connection.Query<int>(itemQuery, new { MenuId = menuId }).ToList();

        foreach (int itemId in itemIds)
        {
            string deleteChoicesQuery = "DELETE FROM GuestChoice WHERE MenuItemId = @Id";
            _connection.Execute(deleteChoicesQuery, new { Id = itemId });

            string deleteAllergensQuery = "DELETE FROM AllergenOnMenu WHERE NameId = @Id";
            _connection.Execute(deleteAllergensQuery, new { Id = itemId });
        }

        string deleteLinkQuery = "DELETE FROM ItemOnMenu WHERE MenuId = @Id";
        _connection.Execute(deleteLinkQuery, new { Id = menuId });

        string deleteItemsQuery = "DELETE FROM MenuItem WHERE MenuId = @Id";
        _connection.Execute(deleteItemsQuery, new { Id = menuId });

        string query = "DELETE FROM Menu WHERE id = @Id";
        _connection.Execute(query, new { Id = menuId });
        return true;
    }
    public List<AllergenModel> GetAllAllergens()
    {
        string query = "SELECT id as Id, Name FROM Allergen;";
        return _connection.Query<AllergenModel>(query).ToList();
    }

    public void LinkAllergensToMenuItem(int menuItemId, List<int> allergenIds)
    {
        string deleteQuery = "DELETE FROM AllergenOnMenu WHERE NameId = @MenuItemId";
        _connection.Execute(deleteQuery, new { MenuItemId = menuItemId });

        if (allergenIds != null && allergenIds.Count > 0)
        {
            string insertQuery = "INSERT INTO AllergenOnMenu (NameId, AllergenId) VALUES (@MenuItemId, @AllergenId)";
            foreach (int allergenId in allergenIds)
            {
                _connection.Execute(insertQuery, new { MenuItemId = menuItemId, AllergenId = allergenId });
            }
        }
    }
}