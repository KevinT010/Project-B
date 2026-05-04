using System.Text;

public class Menu
{
    private MenuLogic _Logic { get; set; }

    public Menu()
    {
        _Logic = new MenuLogic();
    }

    public void Start()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string prompt = "Menu's";

        List<MenuModel> allMenuItems = _Logic.GetAllMenuItems();

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
            Console.WriteLine("No menu's available.");
            Thread.Sleep(2000);
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
            Console.WriteLine("No menu's available.");
            Thread.Sleep(2000);
        }
        else
        {
            StartMenu.Start();
        }
    }

    public List<string> GetUniqueMenuNames(List<MenuModel> allMenuItems)
    {
        List<string> optionsList = new List<string>();
        foreach (MenuModel menuModel in allMenuItems)
        {
            if (menuModel.IsActive && !string.IsNullOrEmpty(menuModel.MenuName) && !optionsList.Contains(menuModel.MenuName))
            {
                optionsList.Add(menuModel.MenuName);
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
        List<string> categoryOrder = new List<string> { "starter", "main course", "kids meal", "dessert", "drinks" };
        List<string> availableCategories = new List<string>();

        foreach (string expectedCategory in categoryOrder)
        {
            foreach (string uniqueCategory in uniqueCategories)
            {
                if (uniqueCategory.ToLower() == expectedCategory.ToLower())
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
        Console.Clear();
        Console.WriteLine($"\n=== {selectedCategory} ===\n");

        foreach (MenuModel item in itemsToDisplay)
        {
            if (item.FoodCategory.ToLower() == selectedCategory.ToLower())
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

        bool Contains(string value) => allergens.ToLower().Contains(value.ToLower());

        if (Contains("milk") || Contains("dairy") || Contains("lactose"))
        {
            emojiList.Add("🥛");
        }
        if (Contains("egg"))
        {
            emojiList.Add("🥚");
        }
        if (Contains("shellfish"))
        {
            emojiList.Add("🦐");
        }
        if (Contains("fish"))
        {
            emojiList.Add("🐟");
        }
        if (Contains("peanuts") || Contains("nuts"))
        {
            emojiList.Add("🥜");
        }
        if (Contains("wheat") || Contains("gluten"))
        {
            emojiList.Add("🌾");
        }
        if (Contains("soy"))
        {
            emojiList.Add("🫘");
        }
        if (Contains("sesame"))
        {
            emojiList.Add("🌱");
        }
        if (Contains("alcohol"))
        {
            emojiList.Add("🍷");
        }
        if (Contains("Crustaceans"))
        {
            emojiList.Add("🦐");
        }


        return emojiList.Count > 0 ? string.Join(" ", emojiList) : "No allergens";
    }
}