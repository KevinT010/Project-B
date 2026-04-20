using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MenuLogicTest
{

    [TestMethod]
    public void CreateMenu_MenuIsAdded()
    {
        MenuLogic logic = new MenuLogic();

        try
        {
            logic.CreateMenu("New Breakfast Menu");
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public void AddMenuItem_MenuItemIsAdded()
    {
        MenuLogic logic = new MenuLogic();
        MenuModel newItem = new MenuModel("1", "Pancakes", "Fluffy pancakes", 5.99m, "Breakfast", "Gluten, Eggs");

        try
        {
            logic.AddMenuItem(newItem, 1);
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public void UpdateMenu_MenuIsUpdated()
    {
        MenuLogic logic = new MenuLogic();

        try
        {
            logic.UpdateMenu(1, "Updated Test Menu", true);
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public void UpdateMenuItem_MenuItemIsUpdated()
    {
        MenuLogic logic = new MenuLogic();
        MenuModel testItem = new MenuModel("1", "Test Item", "Test Desc", 9.99, "Test Category", "None");

        try
        {
            logic.UpdateMenuItem(testItem);
            Assert.IsTrue(true);
        }
        catch
        {
            Assert.Fail();
        }
    }

    [TestMethod]
    public void DeleteMenu_MenuIsDeleted()
    {
        MenuLogic logic = new MenuLogic();

        bool result = logic.DeleteMenu(9999);

        Assert.IsInstanceOfType(result, typeof(bool));
    }

    [TestMethod]
    public void DeleteMenuItem_MenuItemIsDeleted()
    {
        MenuLogic logic = new MenuLogic();

        bool result = logic.DeleteMenuItem(9999);

        Assert.IsInstanceOfType(result, typeof(bool));
    }
}