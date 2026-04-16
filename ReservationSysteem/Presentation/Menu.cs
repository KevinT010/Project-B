using System.Text;

public class Menu
{
    public MenuLogic Logic { get; set; }

    public Menu()
    {
        Logic = new MenuLogic();
    }

    public void Start()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string prompt = "Menu's";

        List<MenuModel> allMenuItems = Logic.GetAllMenuItems();
        
        if (allMenuItems.Count == 0)
        {
            HandleEmptyMenu();
            return;
        }

        List<string> optionsList = GetUniqueMenuNames(allMenuItems);
        
        if (optionsList.Count == 0)
        {
            HandleEmptyMenu();
            return;
        }

        optionsList.Add("Return to start");

        string[] options = optionsList.ToArray();
        Ui MenuUi = new Ui(prompt, options);
        int selectedIndex = MenuUi.Run();

        if (options[selectedIndex] == "Return to start")
        {
            ReturnToPreviousMenu();
            return;
        }

        if (selectedIndex >= 0 && selectedIndex < options.Length)
        {
            string selectedMenuName = options[selectedIndex];
            List<MenuModel> itemsToDisplay = GetItemsForSelectedMenu(allMenuItems, selectedMenuName);
            SubMenu(itemsToDisplay);
        }
    }

    public void HandleEmptyMenu()
    {
        if (Session.CurrentUser != null)
        {
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
        }
        else
        {
            Console.WriteLine("No menu's available.");
            Thread.Sleep(2000);
            StartMenu.Start();
        }
    }

    public void ReturnToPreviousMenu()
    {
        if (Session.CurrentUser != null)
        {
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
        }
        else
        {
            StartMenu.Start();
        }
    }

    public List<string> GetUniqueMenuNames(List<MenuModel> allMenuItems)
    {
        List<string> optionsList = new List<string>();
        foreach (MenuModel m in allMenuItems)
        {
            if (m.IsActive && !string.IsNullOrEmpty(m.MenuName) && !optionsList.Contains(m.MenuName))
            {
                optionsList.Add(m.MenuName);
            }
        }
        return optionsList;
    }

    public List<MenuModel> GetItemsForSelectedMenu(List<MenuModel> allMenuItems, string selectedMenuName)
    {
        List<MenuModel> itemsToDisplay = new List<MenuModel>();
        foreach (MenuModel item in allMenuItems)
        {
            if (item.MenuName == selectedMenuName)
            {
                itemsToDisplay.Add(item);
            }
        }
        return itemsToDisplay;
    }

    public void SubMenu(List<MenuModel> itemsToDisplay)
    {
        List<string> uniqueCategories = GetUniqueCategories(itemsToDisplay);
        List<string> availableCategories = GetOrderedAvailableCategories(uniqueCategories);
        availableCategories.Add("Return to menu's");

        string[] categoryOptions = availableCategories.ToArray();
        Ui categoryMenu = new Ui("Select a Category", categoryOptions);
        int categoryIndex = categoryMenu.Run();

        if (categoryOptions[categoryIndex] == "Return to menu's")
        {
            Start();
            return;
        }

        string selectedCategory = categoryOptions[categoryIndex];
        DisplayItemsInCategory(itemsToDisplay, selectedCategory);

        Console.WriteLine("Press any key to return to the category selection...");
        Console.ReadKey();
        SubMenu(itemsToDisplay); 
    }

    public List<string> GetUniqueCategories(List<MenuModel> itemsToDisplay)
    {
        List<string> uniqueCategories = new List<string>();
        foreach (MenuModel item in itemsToDisplay)
        {
            if (!string.IsNullOrEmpty(item.FoodCategory) && !uniqueCategories.Contains(item.FoodCategory))
            {
                uniqueCategories.Add(item.FoodCategory);
            }
        }
        return uniqueCategories;
    }

    public List<string> GetOrderedAvailableCategories(List<string> uniqueCategories)
    {
        List<string> categoryOrder = new List<string> { "Starter", "Main Course", "Kids Meal", "Dessert", "Drinks" };
        List<string> availableCategories = new List<string>();

        foreach (string expectedCategory in categoryOrder)
        {
            foreach (string uniqueCategory in uniqueCategories)
            {
                if (uniqueCategory.Equals(expectedCategory, StringComparison.OrdinalIgnoreCase))
                {
                    availableCategories.Add(expectedCategory);
                    break;
                }
            }
        }
        return availableCategories;
    }

    public void DisplayItemsInCategory(List<MenuModel> itemsToDisplay, string selectedCategory)
    {
        Console.WriteLine($"\n=== {selectedCategory} ===\n");

        foreach (MenuModel item in itemsToDisplay)
        {
            if (item.FoodCategory.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price}");

                if (!string.IsNullOrEmpty(item.Allergens))
                {
                    string emoji = GetAllergenEmojis(item.Allergens);
                    Console.WriteLine($"Allergens: {emoji}");
                }
                Console.WriteLine("-----------------------------");
            }
        }
    }

    public string GetAllergenEmojis(string allergens)
    {
        if (string.IsNullOrWhiteSpace(allergens))
            return "No allergens";

        var emojiList = new List<string>();

        bool Has(string value) => allergens.IndexOf(value, StringComparison.OrdinalIgnoreCase) >= 0;

        if (Has("Milk") || Has("Dairy") || Has("Lactose")) emojiList.Add("🥛");
        if (Has("Egg")) emojiList.Add("🥚");
        if (Has("Shellfish")) emojiList.Add("🦐");
        if (Has("Fish")) emojiList.Add("🐟");
        if (Has("Peanuts") || Has("Nuts")) emojiList.Add("🥜");
        if (Has("Wheat") || Has("Gluten")) emojiList.Add("🌾");
        if (Has("Soy")) emojiList.Add("🫘");
        if (Has("Sesame")) emojiList.Add("🌱");

        return emojiList.Count > 0 ? string.Join(" ", emojiList) : "No allergens";
    }
}