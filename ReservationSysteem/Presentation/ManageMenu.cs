public class ManageMenu
{
    public MenuLogic Logic { get; set; }

    public ManageMenu()
    {
        Logic = new MenuLogic();
    }

    public void Start()
    {
        string[] options = { "Create menu", "Edit menu", "Delete menu", "Add menu item", "Edit menu item", "Delete menu item", "Go back to main menu" };
        Ui ui = new Ui("Manage menu's & items", options);
        int choice = ui.Run();

        switch (choice)
        {
            case 0:
                CreateMenu();
                break;
            case 1:
                EditMenu();
                break;
            case 2:
                DeleteMenu();
                break;
            case 3:
                AddMenuItem();
                break;
            case 4:
                EditMenuItem();
                break;
            case 5:
                DeleteMenuItem();
                break;
            case 6:
                Return();
                break;
        }
    }

    public void CreateMenu()
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

    public void EditMenu()
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

        Ui menuSelection = new Ui("Select the menu to edit:", menuOptions);
        int selectedIndex = menuSelection.Run();

        if (selectedIndex == menus.Count)
        {
            Start();
            return;
        }

        MenuModel selectedMenu = menus[selectedIndex];

        Console.Clear();
        Console.Write($"Enter new menu name or press Enter to keep current name: ");
        string newName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newName))
        {
            newName = selectedMenu.MenuName;
        }

        Console.Write("Should this menu be active? (y/n): ");
        bool isActive = Console.ReadLine()?.Trim().ToLower() == "y";

        Logic.UpdateMenu(selectedMenu.Id, newName, isActive);
        Console.WriteLine("Menu updated.");
        Thread.Sleep(2000);
        Start();
    }

    public void DeleteMenu()
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

    public void AddMenuItem()
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

        decimal price;
        Console.Write("Price: ");
        while (!decimal.TryParse(Console.ReadLine(), out price) || price < 0)
        {
            Console.WriteLine("Invalid input. Please enter a valid number.");
            Console.Write("Price: ");
        }
        
        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("Category: ");
        string category = Console.ReadLine() ?? "";

        while (string.IsNullOrWhiteSpace(category))
        {
            Console.Write("Category cannot be empty. Enter category: ");
            category = Console.ReadLine() ?? "";
        }
        Console.WriteLine($"Selected Category: {category}");
        
        Console.Write("Allergens (use , if you want multiple or leave empty): ");
        string allergens = Console.ReadLine() ?? "";

        MenuModel newItem = new MenuModel("", name, description, price, category, allergens);
        long newItemId = Logic.AddMenuItem(newItem, selectedMenuId);
        
        Logic.LinkItemToMenu(newItemId, selectedMenuId);
        
        Console.Clear();
        Console.WriteLine("Menu item added successfully!");
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Name:        {name}");
        Console.WriteLine($"Price:       €{price:0.00}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Category:    {category}");
        Console.WriteLine($"Allergens:   {allergens}");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
        Start();
    }

    public void EditMenuItem()
    {
        Console.Clear();
        List<MenuModel> items = Logic.GetAllMenuItems();
        
        if (items.Count == 0)
        {
            Console.WriteLine("No menu items exist.");
            Thread.Sleep(2000); 
            Start();
            return;
        }

        Console.Write("Enter the name of the item to edit: ");
        string itemName = Console.ReadLine() ?? "";

        MenuModel selectedItem = null;

        foreach (var item in items)
        {
            if (item.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
            {
                selectedItem = item;
                break;
            }
        }

        if (selectedItem == null)
        {
            Console.WriteLine("Menu item not found.");
            Thread.Sleep(2000);
            Start();
            return;
        }

        Console.Clear();
        
        Console.Write($"Name: ");
        string newName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newName))
        {
            selectedItem.Name = newName;
        }

        Console.Write($"Price: ");
        string priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal newPrice) && newPrice >= 0)
        {
            selectedItem.Price = newPrice;
        }

        Console.Write($"Description: ");
        string newDescription = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newDescription))
        {
            selectedItem.Description = newDescription;
        }

        Console.Write($"Category: ");
        string newCategory = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newCategory))
        {
            selectedItem.FoodCategory = newCategory;
        }

        Console.Write($"Allergens: ");
        string newAllergens = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newAllergens))
        {
            selectedItem.Allergens = newAllergens;
        }

        Logic.UpdateMenuItem(selectedItem);
        Console.Clear();
        Console.WriteLine("Menu item updated successfully!");
        Console.WriteLine("-----------------------------");
        Console.WriteLine($"Name:        {selectedItem.Name}");
        Console.WriteLine($"Price:       €{selectedItem.Price:0.00}");
        Console.WriteLine($"Description: {selectedItem.Description}");
        Console.WriteLine($"Category:    {selectedItem.FoodCategory}");
        Console.WriteLine($"Allergens:   {selectedItem.Allergens}");
        Console.WriteLine("-----------------------------");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
        Start();
    }

    public void DeleteMenuItem()
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

    public void Return()
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