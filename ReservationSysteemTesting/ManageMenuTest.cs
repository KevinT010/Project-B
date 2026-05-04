[TestClass]
public class MenuLogicTest
{
    [TestMethod]
    public void CreateMenu_MenuIsAdded()
    {
        MenuLogic manageMenu = new MenuLogic();
        string name = "test menu";
        bool isActive = true;

        manageMenu.CreateMenu(name, isActive);

        string actualMenuName = manageMenu.GetAllMenus()[^1].MenuName;

        Assert.AreEqual(name, actualMenuName);
    }

    [TestMethod]
    public void AddMenuItem_MenuItemIsAdded()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu", true);
        int menuId = (int)manageMenu.GetAllMenus()[^1].Id;
        string name = "kunpao chicken";

        MenuModel newItem = new MenuModel("test menu", name, "Delicious kunpao chicken", 5.99, "Main Course", "Gluten, Eggs");
        manageMenu.AddMenuItem(newItem, menuId);

        string actualName = manageMenu.GetAllMenuItems()[^1].Name;

        Assert.AreEqual(name, actualName);
    }

    [TestMethod]
    public void UpdateMenu_MenuIsUpdated()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("Initial Menu", false);
        int menuId = (int)manageMenu.GetAllMenus()[^1].Id;

        manageMenu.UpdateMenu(menuId, "Test menu update", true);

        string expected = "Test menu update";
        string actual = manageMenu.GetAllMenus()[^1].MenuName;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void UpdateMenuItem_MenuItemIsUpdated()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu", true);
        int menuId = (int)manageMenu.GetAllMenus()[^1].Id;
        MenuModel testItem = new MenuModel("test menu", "kunpao chicken update", "Delicious kunpao chicken", 5.99, "Main Course", "Gluten, Eggs");
        manageMenu.AddMenuItem(testItem, menuId);
        
        MenuModel addedItem = manageMenu.GetAllMenuItems()[^1];
        addedItem.Name = "chicken update";

        manageMenu.UpdateMenuItem(addedItem);

        string expected = "chicken update";
        string actual = manageMenu.GetAllMenuItems()[^1].Name;

        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DeleteMenu_MenuIsDeleted()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu delete", true);
        int menuId = (int)manageMenu.GetAllMenus()[^1].Id;

        bool actual = manageMenu.DeleteMenu(menuId);

        bool expected = true;
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void DeleteMenuItem_MenuItemIsDeleted()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu delete item", true);
        int menuId = (int)manageMenu.GetAllMenus()[^1].Id;
        MenuModel testItem = new MenuModel("test menu", "kunpao chicken delete", "Delicious kunpao chicken", 5.99, "Main Course", "Gluten, Eggs");
        manageMenu.AddMenuItem(testItem, menuId);
        int itemId = (int)manageMenu.GetAllMenuItems()[^1].Id;

        bool actual = manageMenu.DeleteMenuItem(itemId);

        bool expected = true;
        Assert.AreEqual(expected, actual);
    }
}