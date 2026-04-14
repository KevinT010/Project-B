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
        var optionsList = allMenuItems.Select(m => m.MenuName).Distinct().ToList();
        optionsList.Add("Return to start");

        string[] options = optionsList.ToArray();

        Ui Menu = new Ui(prompt, options);
        int selectedIndex = Menu.Run();

        if (options[selectedIndex] == "Return to start")
        {
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
            return;
        }

        if (selectedIndex >= 0 && selectedIndex < options.Length)
        {

            string selectedMenuName = options[selectedIndex];

            var categoryOrder = new List<string> { "Starter", "Main Course","Kids Meal", "Dessert", "Drinks" };

            var itemsToDisplay = allMenuItems
                .Where(menu => menu.MenuName == selectedMenuName)
                .OrderBy(menu =>
                {
                    int index = categoryOrder.IndexOf(menu.FoodCategory);
                    return index == -1 ? int.MaxValue : index;
                })
                .ToList();

            string currentCategory = "";

            foreach (var item in itemsToDisplay)
            {
                if (currentCategory != item.FoodCategory)
                {

                    currentCategory = item.FoodCategory;
                    Console.WriteLine($"\n=== {currentCategory} ===");
                }

                Console.WriteLine($"Name: {item.Name}");
                Console.WriteLine($"Description: {item.Description}");
                Console.WriteLine($"Price: {item.Price}");
                if (!string.IsNullOrEmpty(item.Allergens))
                {
                    var emojiList = new List<string>();
                    string allergens = item.Allergens ?? string.Empty;

                    if (allergens.Contains("Milk") || allergens.Contains("Dairy")) emojiList.Add("🥛");
                    if (allergens.Contains("Egg")) emojiList.Add("🥚");
                    if (allergens.Contains("Shellfish")) emojiList.Add("🦐");
                    if (allergens.Contains("Fish")) emojiList.Add("🐟");
                    if (allergens.Contains("Nuts")) emojiList.Add("🥜");
                    if (allergens.Contains("Wheat") || allergens.Contains("Gluten")) emojiList.Add("🌾");
                    if (allergens.Contains("Soy")) emojiList.Add("🫘");
                    if (allergens.Contains("Sesame")) emojiList.Add("🌱");
                    if (emojiList.Count == 0)
                    {
                        emojiList.Add("No allergens");
                    }

                    string emoji = string.Join(" ", emojiList);

                    Console.WriteLine($"Allergens: {emoji}");
                }
                Console.WriteLine("-----------------------------");
            }

            Console.WriteLine("Press any key to return to the other menus...");
            Console.ReadKey();
            Start();
            return;

        }


    }
}