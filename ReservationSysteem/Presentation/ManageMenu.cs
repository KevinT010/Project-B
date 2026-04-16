public class ManageMenu
{
    public MenuLogic Logic { get; set; }

    public ManageMenu()
    {
        Logic = new MenuLogic();
    }

    public void Start()
    {
        string[] options = { "Create Menu", "Delete Menu", "Add Menu Item", "Delete Menu Item", "go back to Main Menu" };
        Ui ui = new Ui("Manage Menus & Items", options);
        int choice = ui.Run();

        switch (choice)
        {
            case 0:
                CreateMenuProcess();
                break;
            case 1:
                DeleteMenuProcess();
                break;
            case 2:
                AddMenuItemProcess();
                break;
            case 3:
                DeleteMenuItemProcess();
                break;
            case 4:
                ReturnProcess();
                break;
        }
    }

    public void CreateMenuProcess()
    {
        Console.Clear();
        Console.Write("Enter new menu name:   (Or press Enter to cancel) ");
        string name = Console.ReadLine();
        
        if (!string.IsNullOrWhiteSpace(name))
        {
            Logic.CreateMenu(name);
            Console.WriteLine("Menu created successfully.");
        }
        
        Thread.Sleep(2000); 
        Start();
    }

    public void DeleteMenuProcess()
    {
        Console.Clear();
        List<MenuModel> menus = Logic.GetAllMenus();
        
        if (menus.Count == 0)
        {
            Console.WriteLine("No menus exist.");
            Thread.Sleep(2000); 
            Start();
            return;
        }

        string[] menuOptions = new string[menus.Count + 1];
        for (int i = 0; i < menus.Count; i++)
        {
            menuOptions[i] = menus[i].MenuName;
        }
        menuOptions[menus.Count] = "Cancel";

        Ui menuSelection = new Ui("Select the menu to delete:", menuOptions);
        int selectedIndex = menuSelection.Run();

        if (selectedIndex == menus.Count)
        {
            Start();
            return;
        }

        long idToDelete = menus[selectedIndex].Id;
        bool success = Logic.DeleteMenu(idToDelete);
        
        if (success)
        {
            Console.WriteLine("Menu deleted.");
        }
        else
        {
            Console.WriteLine("Cannot delete menu: items are currently in a reservation.");
        }
        
        Thread.Sleep(2000); 
        Start();
    }

    public void AddMenuItemProcess()
    {
        Console.Clear();
        List<MenuModel> menus = Logic.GetAllMenus();
        
        if (menus.Count == 0)
        {
            Console.WriteLine("Please create a menu first.");
            Thread.Sleep(2000); 
            Start();
            return;
        }

        string[] menuOptions = new string[menus.Count + 1];
        for (int i = 0; i < menus.Count; i++)
        {
            menuOptions[i] = menus[i].MenuName;
        }
        menuOptions[menus.Count] = "Go back";

        Ui menuSelection = new Ui("Select the menu:", menuOptions);
        int selectedMenuIndex = menuSelection.Run();

        if (selectedMenuIndex == menus.Count)
        {
            Start();
            return;
        }

        long selectedMenuId = menus[selectedMenuIndex].Id;

        Console.Clear();
        string name = GetUniqueItemName();
        decimal price = GetValidPrice();
        
        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        string category = GetValidCategory();
        Console.WriteLine($"Selected Category: {category}");
        
        Console.Write("Allergens (comma separated, or leave empty): ");
        string allergens = Console.ReadLine() ?? "";

        MenuModel newItem = new MenuModel("", name, description, price, category, allergens);
        long newItemId = Logic.AddMenuItem(newItem, selectedMenuId);
        
        Logic.LinkItemToMenu(newItemId, selectedMenuId);
        
        PrintItemSuccess(name, price, description, category, allergens);
        Start();
    }

    public string GetUniqueItemName()
    {
        Console.Write("Name: ");
        string name;
        while (true)
        {
            name = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.Write("Name cannot be empty. Enter name: ");
                continue;
            }

            bool exists = false;
            foreach (var item in Logic.GetAllMenuItems())
            {
                if (item.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                break;
            }

            Console.Write("This name already exists. Enter a different name: ");
        }
        return name;
    }

    public decimal GetValidPrice()
    {
        decimal price;
        Console.Write("Price: ");
        while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            Console.Write("Price: ");
        }
        return price;
    }

    public string GetValidCategory()
    {
        Console.Write("Category: ");
        string category = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(category))
        {
            Console.Write("Category cannot be empty. Enter category: ");
            category = Console.ReadLine() ?? "";
        }
        return category;
    }

    public void PrintItemSuccess(string name, decimal price, string description, string category, string allergens)
    {
        Console.Clear();
        Console.WriteLine("Menu item added successfully!");
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Name:        {name}");
        Console.WriteLine($"Price:       {price}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Category:    {category}");
        Console.WriteLine($"Allergens:   {allergens}");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    public void DeleteMenuItemProcess()
    {
        Console.Clear();
        Console.Write("Enter the name of the item to delete: ");
        string itemName = Console.ReadLine() ?? "";

        List<MenuModel> allItems = Logic.GetAllMenuItems();
        MenuModel itemToDelete = null;

        foreach (var item in allItems)
        {
            if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                itemToDelete = item;
                break;
            }
        }

        if (itemToDelete != null)
        {
            bool success = Logic.DeleteMenuItem(itemToDelete.Id);
            if (success)
            {
                Console.WriteLine($"Item '{itemToDelete.Name}' deleted.");
            }
            else
            {
                Console.WriteLine("Cannot delete item: it is currently in a reservation.");
            }
        }
        else
        {
            Console.WriteLine("Menu item not found.");
        }

        Thread.Sleep(2000);
        Start();
    }

    public void ReturnProcess()
    {
        Console.Clear();
        if (Session.CurrentUser != null)
        {
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
        }
        else
        {
            StartMenu.Start();
        }
    }
}