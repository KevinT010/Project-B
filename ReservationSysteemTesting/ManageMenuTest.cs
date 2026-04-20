using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        var menus = manageMenu.GetAllMenus();
        var addedMenu = menus.Find(m => m.MenuName == name);

        Assert.IsNotNull(addedMenu);
        Assert.AreEqual(name, addedMenu.MenuName);
    }

    [TestMethod]
    public void AddMenuItem_MenuItemIsAdded()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu", true);
        
        var menus = manageMenu.GetAllMenus();
        var menu = menus.Find(m => m.MenuName == "test menu");
        int menuId = (int)menu.Id;

        MenuModel newItem = new MenuModel("test menu", "kunpao chicken", "Delicious kunpao chicken", 5.99, "Main Course", "Gluten, Eggs");

        manageMenu.AddMenuItem(newItem, menuId);

        var items = manageMenu.GetAllMenuItems();
        var addedItem = items.Find(i => i.Name == "kunpao chicken");

        Assert.IsNotNull(addedItem);
        Assert.AreEqual("kunpao chicken", addedItem.Name);
    }

    [TestMethod]
    public void UpdateMenu_MenuIsUpdated()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("Initial Menu", false);

        var menus = manageMenu.GetAllMenus();
        var menu = menus.Find(m => m.MenuName == "Initial Menu");

        manageMenu.UpdateMenu((int)menu.Id, "Updated Test Menu", true);

        menus = manageMenu.GetAllMenus();
        var updatedMenu = menus.Find(m => m.Id == menu.Id);

        Assert.IsNotNull(updatedMenu);
        Assert.AreEqual("Updated Test Menu", updatedMenu.MenuName);
    }

    [TestMethod]
    public void UpdateMenuItem_MenuItemIsUpdated()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu", true);
        
        var menus = manageMenu.GetAllMenus();
        var menu = menus.Find(m => m.MenuName == "test menu");
        int menuId = (int)menu.Id;

        MenuModel testItem = new MenuModel("test menu", "kunpao chicken update", "Delicious kunpao chicken", 5.99, "Main Course", "Gluten, Eggs");
        manageMenu.AddMenuItem(testItem, menuId);

        var items = manageMenu.GetAllMenuItems();
        var addedItem = items.Find(i => i.Name == "kunpao chicken update");

        addedItem.Name = "Updated Chicken";
        manageMenu.UpdateMenuItem(addedItem);

        items = manageMenu.GetAllMenuItems();
        var updatedItem = items.Find(i => i.Id == addedItem.Id);

        Assert.IsNotNull(updatedItem);
        Assert.AreEqual("Updated Chicken", updatedItem.Name);
    }

    [TestMethod]
    public void DeleteMenu_MenuIsDeleted()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu delete", true);

        var menus = manageMenu.GetAllMenus();
        var menu = menus.Find(m => m.MenuName == "test menu delete");

        bool result = manageMenu.DeleteMenu((int)menu.Id);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DeleteMenuItem_MenuItemIsDeleted()
    {
        MenuLogic manageMenu = new MenuLogic();
        manageMenu.CreateMenu("test menu delete item", true);
        
        var menus = manageMenu.GetAllMenus();
        var menu = menus.Find(m => m.MenuName == "test menu");
        int menuId = (int)menu.Id;

        MenuModel testItem = new MenuModel("test menu", "kunpao chicken delete", "Delicious kunpao chicken", 5.99, "Main Course", "Gluten, Eggs");
        manageMenu.AddMenuItem(testItem, menuId);

        var items = manageMenu.GetAllMenuItems();
        var addedItem = items.Find(i => i.Name == "kunpao chicken delete");

        bool result = manageMenu.DeleteMenuItem((int)addedItem.Id);

        Assert.IsTrue(result);
    }
}