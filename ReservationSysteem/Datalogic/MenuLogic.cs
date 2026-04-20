public class MenuLogic
{
    private MenuAccess _access = new();

    public MenuLogic()
    {
    }

    public void AddMenuItem(MenuModel menuItem, int menuId)
    {
        int newItemId = _access.InsertMenuItem(menuItem, menuId);
        _access.LinkItemToMenu(newItemId, menuId);
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

    public void CreateMenu(string menuName)
    {
        _access.CreateMenu(menuName);
    }

    public void UpdateMenu(int menuId, string newMenuName, bool isActive)
    {
        _access.UpdateMenu(menuId, newMenuName, isActive);
    }

    public void UpdateMenuItem(MenuModel menuItem)
    {
        _access.UpdateMenuItem(menuItem);
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
}