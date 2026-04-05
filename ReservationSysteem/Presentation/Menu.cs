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
        string[] options = allMenuItems.Select(menu => menu.MenuName).Distinct().ToArray();

        Ui Menu = new Ui(prompt, options);
        int selectedIndex = Menu.Run();

        if (selectedIndex == 0)
        {
            string selectedMenuName = options[selectedIndex];

            var categoryOrder = new List<string> { "Starter", "Main Course", "Dessert", "Drink" };

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
                    if (allergens.Contains("Tree nut")) emojiList.Add("🌰");
                    if (allergens.Contains("Peanut")) emojiList.Add("🥜");
                    if (allergens.Contains("Wheat") || allergens.Contains("Gluten")) emojiList.Add("🌾");
                    if (allergens.Contains("Soy")) emojiList.Add("🫘");
                    if (allergens.Contains("Sesame")) emojiList.Add("🌱");

                    string emoji = string.Join(" ", emojiList);

                    Console.WriteLine($"Allergens: {item.Allergens} {emoji}");
                }

                Console.WriteLine(new string('-', 30));
            }
        }
        if (selectedIndex == 1)
        {
            string selectedMenuName = options[selectedIndex];

            var categoryOrder = new List<string> { "Starter", "Main Course", "Dessert", "Drink" };

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
                    if (allergens.Contains("Tree nut")) emojiList.Add("🌰");
                    if (allergens.Contains("Peanut")) emojiList.Add("🥜");
                    if (allergens.Contains("Wheat") || allergens.Contains("Gluten")) emojiList.Add("🌾");
                    if (allergens.Contains("Soy")) emojiList.Add("🫘");
                    if (allergens.Contains("Sesame")) emojiList.Add("🌱");

                    string emoji = string.Join(" ", emojiList);

                    Console.WriteLine($"Allergens: {item.Allergens} {emoji}");
                }

                Console.WriteLine(new string('-', 30));
            }
        }

    }
}