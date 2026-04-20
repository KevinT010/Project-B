public class MenuLogic
{
    private MenuAccess _access = new();

    public MenuLogic()
    {
    }

    public long AddMenuItem(MenuModel menuItem, long menuId)
    {
        return _access.InsertMenuItem(menuItem, menuId);
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

    public void UpdateMenu(long menuId, string newMenuName, bool isActive)
    {
        _access.UpdateMenu(menuId, newMenuName, isActive);
    }

    public void UpdateMenuItem(MenuModel menuItem)
    {
        _access.UpdateMenuItem(menuItem);
    }

    public bool DeleteMenuItem(long menuItemId)
    {
        return _access.DeleteMenuItem(menuItemId);
    }

    public bool DeleteMenu(long menuId)
    {
        return _access.DeleteMenu(menuId);
    }
    
    public void LinkItemToMenu(long menuItemId, long menuId)
    {
        _access.LinkItemToMenu(menuItemId, menuId);
    }
}