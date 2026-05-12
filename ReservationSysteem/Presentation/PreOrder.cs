using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

public class PreOrder
{

    private List<GuestChoiceModel> AllSelectedItems = [];
    private PreOrderLogic Logic = new PreOrderLogic();
    private MenuLogic MenuLogic = new MenuLogic();
    private RewardLogic RewardLogic = new();
    private ViewReservations viewReservations = new();

    public void Start(AccountModel account, ReservationModel pickedReservation)
    {
        SelectGuestMenu(account, pickedReservation);
    }

    public void SelectGuestMenu(AccountModel account, ReservationModel pickedReservation)
    {
        List<string> guest = [];

        for (int i = 1; i <= pickedReservation.NumberOfGuests; i++)
        {
            guest.Add($"Guest: {i}");
        }
        guest.Add("View Order");
        guest.Add("Edit Order");
        guest.Add("Confirm Order");
        guest.Add("Back");

        Ui PreOrderMenu = new Ui("Select Guest", guest.ToArray());
        int SelectedIndex = PreOrderMenu.Run();

        switch (guest[SelectedIndex])
        {
            case "View Order":
                ViewOrder();
                SelectGuestMenu(account, pickedReservation);
                return;
            case "Edit Order":
                EditOrder();
                SelectGuestMenu(account, pickedReservation);
                return;
            case "Confirm Order":
                ConfirmOrder(account, pickedReservation);
                return;
            case "Back":
                viewReservations.Start(account);
                return;
            default:
                int guestNumber = SelectedIndex + 1;
                SelectAllergens(account, pickedReservation, guestNumber);
                return;
        }
    }

    public void SelectAllergens(AccountModel account, ReservationModel pickedReservation, int guestNumber)
    {

        List<string> allergenOptions = ["Milk / Dairy", "Egg", "Shellfish", "Fish", "Peanuts / Nuts", "Wheat / Gluten", "Soy", "Sesame", "Alcohol", "None", "Remove item", "Back", "Done"];
        List<string> chosenAllergens = [];
        
        GuestModel? existingGuest = Logic.GetGuest(pickedReservation.Id, guestNumber);

        // check if guest exist
        if (existingGuest != null)
        {
            Console.Clear();
            Console.WriteLine($"Guest {guestNumber}");

            if (string.IsNullOrEmpty(existingGuest.Allergens))
            {
                Console.WriteLine("Allergens: none");
            }
            else
            {
                Console.WriteLine($"Allergens: {existingGuest.Allergens}");
            }
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();

            SelectMenuItem(account, pickedReservation, guestNumber);
            return;
        }

        // does not exitst
        while (true)
        {
            Ui menu = new Ui("Select allergen", allergenOptions.ToArray());
            int SelectedIndex = menu.Run();
            string choice = allergenOptions[SelectedIndex];

            if (choice == "Back")
            {
                SelectGuestMenu(account, pickedReservation);
                return;
            }

            if (choice == "None")
            {
                chosenAllergens.Clear();
                continue;
            }

            if (choice == "Remove item")
            {
                if (chosenAllergens.Count == 0)
                {
                    continue;
                }

                Ui removeMenu = new Ui("Remove allergen", chosenAllergens.ToArray());
                int removeIndex = removeMenu.Run();

                chosenAllergens.RemoveAt(removeIndex);
                continue;
            }


            if (choice == "Done")
            {
                string allergens = string.Join(", ", chosenAllergens);

                Logic.MakeGuest(pickedReservation.Id, guestNumber, allergens);
                SelectMenuItem(account, pickedReservation, guestNumber);
                return;
            }

            if (!chosenAllergens.Contains(choice))
            {
                chosenAllergens.Add(choice);
            }
        }
    }

    public void SelectMenuItem(AccountModel account, ReservationModel pickedReservation, int guestNumber)
    {
        // Pick menu
        List<MenuModel> allMenus = MenuLogic.GetAllMenus();
        List<string> menuOptions = [];

        foreach (MenuModel menu in allMenus)
        {
            menuOptions.Add(menu.MenuName);
        }
        menuOptions.Add("Back");

        Ui MenuList = new Ui("Select menu", menuOptions.ToArray());
        int menuSelected = MenuList.Run();

        if (menuOptions[menuSelected] == "Back")
        {
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        MenuModel selectedMenu = allMenus[menuSelected];

        // Pick category
        List<MenuModel> allMenuItems = MenuLogic.GetAllMenuItems();
        List<MenuModel> itemsInMenu = [];

        foreach (MenuModel item in allMenuItems)
        {
            if (item.MenuName == selectedMenu.MenuName)
            {
                itemsInMenu.Add(item);
            }
        }

        List<string> categoryOptions = [];

        foreach (MenuModel item in itemsInMenu)
        {
            if (!categoryOptions.Contains(item.FoodCategory))
            {
                categoryOptions.Add(item.FoodCategory);
            }
        }
        categoryOptions.Add("Back");

        Ui CategoryList = new Ui("Select category", categoryOptions.ToArray());
        int selectedCategoryIndex = CategoryList.Run();

        if (categoryOptions[selectedCategoryIndex] == "Back")
        {
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        string selectedCategory = categoryOptions[selectedCategoryIndex];

        // Pick item
        List<MenuModel> itemsInCategory = new();
        foreach (MenuModel item in itemsInMenu)
        {
            if (item.FoodCategory == selectedCategory)
            {
                itemsInCategory.Add(item);
            }
        }

        List<string> itemOptions = new();
        foreach (MenuModel item in itemsInCategory)
        {
            itemOptions.Add($"{item.Name} - ${item.Price}");
        }
        itemOptions.Add("Back");

        Ui ItemList = new Ui("Select item", itemOptions.ToArray());
        int selectedItemIndex = ItemList.Run();

        if (itemOptions[selectedItemIndex] == "Back")
        {
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        MenuModel selectedItem = itemsInCategory[selectedItemIndex];

        GuestModel? guest = Logic.GetGuest(pickedReservation.Id, guestNumber);

        if (guest == null)
        {
            Console.WriteLine("Guest not found.");
            Console.ReadKey();
            return;
        }

        GuestChoiceModel? existing = null;

        foreach (GuestChoiceModel choice in AllSelectedItems)
        {
            if (choice.GuestId == guest.Id && choice.MenuItemId == selectedItem.Id)
            {
                existing = choice;
                break;
            }
        }
        if (existing != null)
        {
            existing.Quantity++;
        }
        else
        {
            AllSelectedItems.Add(new GuestChoiceModel(selectedItem.Id, guest.Id, 1));
        }

        Console.Clear();
        Console.WriteLine($"Added: {selectedItem.Name} for Guest {guestNumber}");
        Console.WriteLine("Press any key");
        Console.ReadKey();

        SelectGuestMenu(account, pickedReservation);
    }

    public void ViewOrder()
    {
        Console.Clear();
        Console.WriteLine("Selected Items");

        if (AllSelectedItems.Count == 0)
        {
            Console.WriteLine("No items selected.");
            Console.WriteLine("Press any key");
            Console.ReadKey();
            return;
        }

        List<MenuModel> allItems = MenuLogic.GetAllMenuItems();

        foreach (GuestChoiceModel order in AllSelectedItems)
        {
            GuestModel? guest = Logic.GetGuestById(order.GuestId);
            MenuModel? item = null;

            foreach (MenuModel i in allItems)
            {
                if (i.Id == order.MenuItemId)
                {
                    item = i;
                    break;
                }
            }
            Console.WriteLine($"Guest {guest.GuestNumber} | {item.Name} - ${item.Price} | x{order.Quantity}");
        }
        Console.WriteLine("Press any key");
        Console.ReadKey();
    }


    public void EditOrder()
    {
        List<MenuModel> allItems = MenuLogic.GetAllMenuItems();
        while (true)
        {
            if (AllSelectedItems.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No items selected.");
                Console.ReadKey();
                return;
            }

            List<string> editOptions = [];

            foreach (GuestChoiceModel order in AllSelectedItems)
            {
                GuestModel? guest = Logic.GetGuestById(order.GuestId);
                MenuModel? item = null;
                foreach (MenuModel i in allItems)
                {
                    if (i.Id == order.MenuItemId)
                    {
                        item = i;
                        break;
                    }
                }
                editOptions.Add($"Guest {guest.GuestNumber} | {item.Name} - ${item.Price} | x{order.Quantity}");
            }
            editOptions.Add("Back");

            Ui EditOrderUi = new Ui("Edit Order - Select item to remove", editOptions.ToArray());
            int selectedIndex = EditOrderUi.Run();

            if (editOptions[selectedIndex] == "Back")
            {
                return;
            }

            GuestChoiceModel selected = AllSelectedItems[selectedIndex];

            if (selected.Quantity > 1)
            {
                selected.Quantity--;
            }
            else
            {
                AllSelectedItems.RemoveAt(selectedIndex);
                Console.Clear();
                Console.WriteLine("Item removed");
                Console.ReadKey();
            }
        }
    }

    public void ConfirmOrder(AccountModel account, ReservationModel pickedReservation)
    {

        // guestChoice table data:
        // - menuItemId 
        // - guestId
        // - quantity 

        Logic.InsertGuestChoices(AllSelectedItems, pickedReservation.Id);
        Console.Clear();
        Console.WriteLine("Order placed!");
        RewardLogic.GivePoints(account, (double)pickedReservation.PriceTotal);
        Console.WriteLine("Press any key");
        Console.ReadKey();
        viewReservations.Start(account);

    }
}
