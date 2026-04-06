public class MenuLogic
{
    private MenuAccess _access = new();

    public MenuLogic()
    {
    }

    public void AddMenuItem(MenuModel menuItem)
    {
        _access.InsertMenuItem(menuItem);
    }

    public List<MenuModel> GetAllMenuItems()
    {
        var menuItems = _access.GetAllMenuItems();
        return menuItems ?? new List<MenuModel>();
    }
}