public class MenuLogic
{
    private MenuAccess _access = new();

    public MenuLogic()
    {
    }

    public void AddMenuItem(MenuModel menuItem, int menuId, List<int> allergenIds)
    {
        int newItemId = _access.InsertMenuItem(menuItem, menuId);
        _access.LinkItemToMenu(newItemId, menuId);
        _access.LinkAllergensToMenuItem(newItemId, allergenIds);
    }

    public List<MenuModel> GetAllMenuItems()
    {
        var menuItems = _access.GetAllMenuItems();
        return menuItems ?? new List<MenuModel>();
    }

    public List<MenuModel> GetAllMenus()
    {
        var menus = _access.GetAllMenus();
        return menus ?? new List<MenuModel>();
    }

    public void CreateMenu(string menuName, bool isActive)
    {
        _access.CreateMenu(menuName, isActive);
    }

    public void UpdateMenu(int menuId, string newMenuName, bool isActive)
    {
        _access.UpdateMenu(menuId, newMenuName, isActive);
    }

    public void UpdateMenuItem(MenuModel menuItem, List<int> allergenIds)
    {
        _access.UpdateMenuItem(menuItem);
        _access.LinkAllergensToMenuItem((int)menuItem.Id, allergenIds);
    }

    public bool DeleteMenuItem(int menuItemId)
    {
        return _access.DeleteMenuItem(menuItemId);
    }

    public bool DeleteMenu(int menuId)
    {
        return _access.DeleteMenu(menuId);
    }

    public void LinkItemToMenu(int menuItemId, int menuId)
    {
        _access.LinkItemToMenu(menuItemId, menuId);
    }
    public List<AllergenModel> GetAllAllergens()
    {
        var allergens = _access.GetAllAllergens();
        return allergens ?? new List<AllergenModel>();
    }
}