public class PreOrder
{
    private List<GuestChoiceModel> AllSelectedItems = new();
    private PreOrderLogic Logic = new PreOrderLogic();
    private MenuLogic MenuLogic = new MenuLogic();
    private RewardLogic RewardLogic = new RewardLogic();
    private ViewReservations viewReservations = new ViewReservations();

    public void Start(AccountModel account, ReservationModel pickedReservation)
    {
        SelectGuestMenu(account, pickedReservation);
    }

    public void SelectGuestMenu(AccountModel account, ReservationModel pickedReservation)
    {
        List<string> guest = new();

        for (int i = 1; i <= pickedReservation.NumberOfGuests; i++)
        {
            guest.Add($"Guest: {i}");
        }

        guest.Add("View Order");
        guest.Add("Edit Order");
        guest.Add("Confirm Order");
        guest.Add("Back");

        Ui menu = new Ui("Select Guest", guest.ToArray());
        int selectedIndex = menu.Run();

        string choice = guest[selectedIndex];

        if (choice == "View Order")
        {
            ViewOrder();
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        if (choice == "Edit Order")
        {
            EditOrder();
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        if (choice == "Confirm Order")
        {
            ConfirmOrder(account, pickedReservation);
            return;
        }

        if (choice == "Back")
        {
            viewReservations.Start(account);
            return;
        }

        SelectAllergens(account, pickedReservation, selectedIndex + 1);
    }

    public void SelectAllergens(AccountModel account, ReservationModel pickedReservation, int guestNumber)
    {
        List<string> allergenOptions = new()
        {
            "Milk / Dairy",
            "Egg",
            "Shellfish",
            "Fish",
            "Peanuts / Nuts",
            "Wheat / Gluten",
            "Soy",
            "Sesame",
            "Alcohol",
            "None",
            "Remove item",
            "Back",
            "Done"
        };

        List<string> chosenAllergens = new();

        GuestModel? existingGuest = Logic.GetGuest(pickedReservation.Id, guestNumber);

        if (existingGuest != null)
        {
            Console.Clear();
            Console.WriteLine($"Guest {guestNumber}");
            Console.WriteLine(string.IsNullOrEmpty(existingGuest.Allergens) ? "Allergens: none" : $"Allergens: {existingGuest.Allergens}");
            Console.ReadKey();

            SelectMenuItem(account, pickedReservation, guestNumber);
            return;
        }

        while (true)
        {
            Ui menu = new Ui("Select allergen", allergenOptions.ToArray());
            string choice = allergenOptions[menu.Run()];

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
                if (chosenAllergens.Count > 0)
                {
                    Ui removeMenu = new Ui("Remove allergen", chosenAllergens.ToArray());
                    chosenAllergens.RemoveAt(removeMenu.Run());
                }
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
        List<MenuModel> allMenus = MenuLogic.GetAllMenus();
        List<string> menuOptions = new();

        foreach (MenuModel menu in allMenus)
        {
            menuOptions.Add(menu.MenuName);
        }

        menuOptions.Add("Back");

        Ui menuUi = new Ui("Select menu", menuOptions.ToArray());
        int menuIndex = menuUi.Run();

        if (menuOptions[menuIndex] == "Back")
        {
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        MenuModel selectedMenu = allMenus[menuIndex];

        List<MenuModel> allItems = MenuLogic.GetAllMenuItems();
        List<MenuModel> itemsInMenu = new();

        foreach (MenuModel item in allItems)
        {
            if (item.MenuName == selectedMenu.MenuName)
            {
                itemsInMenu.Add(item);
            }
        }

        List<string> categoryOptions = new();

        foreach (MenuModel item in itemsInMenu)
        {
            if (!categoryOptions.Contains(item.FoodCategory))
            {
                categoryOptions.Add(item.FoodCategory);
            }
        }

        categoryOptions.Add("Back");

        Ui categoryUi = new Ui("Select category", categoryOptions.ToArray());
        int categoryIndex = categoryUi.Run();

        if (categoryOptions[categoryIndex] == "Back")
        {
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        string category = categoryOptions[categoryIndex];

        List<MenuModel> items = new();
        List<string> itemOptions = new();

        foreach (MenuModel item in itemsInMenu)
        {
            if (item.FoodCategory == category)
            {
                items.Add(item);
                itemOptions.Add($"{item.Name} - ${item.Price}");
            }
        }

        itemOptions.Add("Back");

        Ui itemUi = new Ui("Select item", itemOptions.ToArray());
        int itemIndex = itemUi.Run();

        if (itemOptions[itemIndex] == "Back")
        {
            SelectGuestMenu(account, pickedReservation);
            return;
        }

        MenuModel selectedItem = items[itemIndex];

        GuestModel? guest = Logic.GetGuest(pickedReservation.Id, guestNumber);

        if (guest == null)
        {
            Console.WriteLine("Guest not found.");
            Console.ReadKey();
            return;
        }

        bool found = false;

        foreach (GuestChoiceModel choice in AllSelectedItems)
        {
            if (choice.GuestId == guest.Id && choice.MenuItemId == selectedItem.Id)
            {
                choice.Quantity++;
                found = true;
                break;
            }
        }

        if (!found)
        {
            AllSelectedItems.Add(new GuestChoiceModel(selectedItem.Id, guest.Id, 1));
        }

        Console.Clear();
        Console.WriteLine($"Added: {selectedItem.Name} for Guest {guestNumber}");
        Console.ReadKey();

        SelectGuestMenu(account, pickedReservation);
    }

    public void ViewOrder()
    {
        Console.Clear();

        if (AllSelectedItems.Count == 0)
        {
            Console.WriteLine("No items selected.");
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

            List<string> editOptions = new();

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

            Ui menu = new Ui("Edit Order - Select item to remove", editOptions.ToArray());
            int index = menu.Run();

            if (editOptions[index] == "Back")
            {
                return;
            }

            GuestChoiceModel selected = AllSelectedItems[index];

            if (selected.Quantity > 1)
            {
                selected.Quantity--;
            }
            else
            {
                AllSelectedItems.RemoveAt(index);
                Console.Clear();
                Console.WriteLine("Item removed");
                Console.ReadKey();
            }
        }
    }

    public void ConfirmOrder(AccountModel account, ReservationModel pickedReservation)
    {
        Logic.InsertGuestChoices(AllSelectedItems, pickedReservation.Id);

        Console.Clear();
        Console.WriteLine("Order placed!");

        RewardLogic.GivePoints(account, (double)pickedReservation.PriceTotal);

        Console.ReadKey();

        viewReservations.Start(account);
    }
}