using System.Text;

public class Menu
{
    public void Start()
    {
        Console.OutputEncoding = Encoding.UTF8;

        string prompt = "Menu's";
        MenuLogic menuLogic = new MenuLogic();

        List<MenuModel> allMenuItems = menuLogic.GetAllMenuItems();
        if (allMenuItems.Count == 0)
        {
            Console.WriteLine("No menu items found in the database.");
            return;
        }

        List<string> optionsList = new List<string>();
        foreach (MenuModel m in allMenuItems)
        {
            if (!optionsList.Contains(m.MenuName))
            {
                optionsList.Add(m.MenuName);
            }
        }
        optionsList.Add("Return to start");

        string[] options = optionsList.ToArray();

        Ui MenuUi = new Ui(prompt, options);
        int selectedIndex = MenuUi.Run();

        if (options[selectedIndex] == "Return to start")
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

        if (selectedIndex >= 0 && selectedIndex < options.Length)
        {
            string selectedMenuName = options[selectedIndex];
            
            List<MenuModel> itemsToDisplay = new List<MenuModel>();
            foreach (MenuModel item in allMenuItems)
            {
                if (item.MenuName == selectedMenuName)
                {
                    itemsToDisplay.Add(item);
                }
            }

            SubMenu(itemsToDisplay);
        }
    }

    public void SubMenu(List<MenuModel> itemsToDisplay)
    {
        List<string> uniqueCategories = new List<string>();
        foreach (MenuModel item in itemsToDisplay)
        {
            if (!uniqueCategories.Contains(item.FoodCategory))
            {
                uniqueCategories.Add(item.FoodCategory);
            }
        }

        List<string> categoryOrder = new List<string> { "Starter", "Main Course", "Kids Meal", "Dessert", "Drinks" };
        List<string> availableCategories = new List<string>();

        foreach (string expectedCategory in categoryOrder)
        {
            if (uniqueCategories.Contains(expectedCategory))
            {
                availableCategories.Add(expectedCategory);
            }
        }

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

        Console.WriteLine($"\n=== {selectedCategory} ===\n");

        foreach (MenuModel item in itemsToDisplay)
        {
            if (item.FoodCategory == selectedCategory)
            {
                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price}");

                if (!string.IsNullOrEmpty(item.Allergens))
                {
                    var emojiList = new List<string>();
                    string allergens = item.Allergens;

                    if (allergens.Contains("Milk") || allergens.Contains("Dairy")) emojiList.Add("🥛");
                    if (allergens.Contains("Egg")) emojiList.Add("🥚");
                    if (allergens.Contains("Shellfish")) emojiList.Add("🦐");
                    if (allergens.Contains("Fish")) emojiList.Add("🐟");
                    if (allergens.Contains("Nuts")) emojiList.Add("🥜");
                    if (allergens.Contains("Wheat") || allergens.Contains("Gluten")) emojiList.Add("🌾");
                    if (allergens.Contains("Soy")) emojiList.Add("🫘");
                    if (allergens.Contains("Sesame")) emojiList.Add("🌱");
                    if (allergens.Contains("Peanuts")) emojiList.Add("🥜");
                    if (allergens.Contains("Lactose")) emojiList.Add("🥛");


                    string emoji = emojiList.Count > 0 ? string.Join(" ", emojiList) : "No allergens";
                    Console.WriteLine($"Allergens: {emoji}");
                }
                Console.WriteLine("-----------------------------");
            }
        }

        Console.WriteLine("Press any key to return to the category selection...");
        Console.ReadKey();
        SubMenu(itemsToDisplay); 
    }
}