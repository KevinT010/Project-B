using System.Buffers;
using System.ComponentModel.DataAnnotations;

public class PreOrder
{
    private List<MenuModel> AllSelectedItems = [];

    private PreOrderLogic Logic = new PreOrderLogic();
    private MenuLogic MenuLogic = new MenuLogic();
    private ViewReservations viewReservations = new();

    public void Start(AccountModel account, ReservationModel pickedReservation)
    {

        // account null check
        // if (account == null)
        // {
        //     return;
        // }


        // // List null check
        // if (reservations.Count == 0)
        // {
        //     Console.WriteLine("No upcoming reservations found.");
        //     Console.ReadKey();
        //     return;
        // }

        // List<string> options = new List<string>();

        // foreach (var r in reservations)
        // {
        //     options.Add($"{r.DateTime:dd-MM-yyyy HH:mm} | Adults: {r.NumberOfGuests - r.NumberOfKids} | Kids: {r.NumberOfKids}");
        // }

        // options.Add("Back");

        // Ui ReservationList = new Ui("Select a reservation for Pre-Order", options.ToArray());
        // int selectedIndexReservation = ReservationList.Run();

        // if (options[selectedIndexReservation] == "Back")
        //     return;

        // ReservationModel selectedReservation = reservations[selectedIndexReservation];

        // Console.Clear();
        // Console.WriteLine($"Selected reservation:");
        // Console.WriteLine($"{pickedReservation.DateTime} - {pickedReservation.NumberOfGuests - pickedReservation.NumberOfKids} adults - {pickedReservation.NumberOfKids} kids");

        // ------------------------------------------------------------------------------------------------------------------------------------------- 
        List<string> Guest = new();
        int GuestCounter = 1;

        for (int i = 0; i < pickedReservation.NumberOfGuests; i++)
        {
            Guest.Add($"Guest: {GuestCounter}");
            GuestCounter++;
        }

        // later .....
        // for (int i = 0; i <= selectedReservation.NumberOfKids; i++)
        // {
        //     Guest.Add($"kid {GuestCounter}");
        //     GuestCounter++;
        // }

        Guest.Add("View Order");
        Guest.Add("Edit Order");
        Guest.Add("Confirm Order");
        Guest.Add("Back");

        Ui GuestList = new Ui("Select Guest", Guest.ToArray());
        int selectedIndexGuest = GuestList.Run();


        if (Guest[selectedIndexGuest] == "View Order")
        {
            // Ui ViewOrder = new Ui("Order List",);
            // if guest selects view order user hass to
            // see a list : MenuItem.Name || price || Guest who ordered it 

            // Use: List<MenuModel> AllSelectedItems = [];
            Console.Clear();
            Console.WriteLine("Selected items:\n");

            if (AllSelectedItems.Count == 0)
            {
                Console.WriteLine("No items selected.");
            }
            else
            {
                foreach (var item in AllSelectedItems)
                {
                    Console.WriteLine($"{item.Name} - €{item.Price}");
                }
            }

            // Console.WriteLine("\nPress any key...");
            // Console.ReadKey();

            return;
        }

        if (Guest[selectedIndexGuest] == "Edit Order")
        {
            // Edit order  is een lijst met alle gekozen items
            // Confirm order
            return;
        }

        if (Guest[selectedIndexGuest] == "Confirm Order")
        {
            // insert
        }


        if (Guest[selectedIndexGuest] == "Back")
        {
            viewReservations.Start(account);
            return;
        }

        // -------------------------------------------------------------------------------------------------------------------------------------------

        List<string> allergenOptions = new() { "Milk / Dairy", "Egg", "Shellfish", "Fish", "Peanuts / Nuts", "Wheat / Gluten", "Soy", "Sesame", "Alcohol", "None", "Remove item", "Back", "Done" };
        List<string> chosenAllergens = new();


        int guestNumber = selectedIndexGuest + 1;
        GuestModel? existingGuest = Logic.GetGuest(pickedReservation.Id, guestNumber);

        if (existingGuest != null)
        {
            Console.Clear();
            Console.WriteLine($"Guest {guestNumber}");

            if (!string.IsNullOrEmpty(existingGuest.Allergens))
            {
                Console.WriteLine($"Allergens: {existingGuest.Allergens}");
                Console.WriteLine();
                Console.WriteLine("Chosen allergens:");
                if (chosenAllergens.Count == 0)
                {
                    Console.WriteLine("- none");
                }
                else
                {
                    chosenAllergens.ForEach(a => Console.WriteLine("- " + a));
                }
            }
            ;

            // int selectedAllergenIndex = AllergensList.Run();
            // string choice = allergenOptions[selectedAllergenIndex];

            // if (choice == "Back")
            // {
            //     Start(account, pickedReservation);
            //     return;
            // }

            // Console.WriteLine("\nPress any key to continue");
            // Console.ReadKey();
        }
        else
        {
            // select multiple times
            while (true)
            {
                Ui AllergensList = new("Select allergen", allergenOptions.ToArray());

                AllergensList.OnAfterDraw = _ =>
                {
                    Console.WriteLine();
                    Console.WriteLine("Chosen allergens:");
                    if (chosenAllergens.Count == 0)
                        Console.WriteLine("- none");
                    else
                        chosenAllergens.ForEach(a => Console.WriteLine("- " + a));
                };

                int selectedAllergenIndex = AllergensList.Run();
                string choice = allergenOptions[selectedAllergenIndex];

                if (choice == "Back")
                {
                    Start(account, pickedReservation);
                    return;
                }

                if (choice == "None")
                {
                    chosenAllergens.Clear();
                    continue;
                }

                if (choice == "Done")
                {
                    // if user is done grab list chosenAllergens and send it too
                    // datalogic layer to make user and continue making the pre-order flow
                    // grab chosenAllergens and pass to datalogic to create guest
                    string? allergens = string.Join(", ", chosenAllergens);
                    Logic.MakeGuest(pickedReservation.Id, selectedIndexGuest + 1, allergens);
                    break;
                    // exit loop continue with showing menu
                }

                if (choice == "Remove item")
                {
                    if (chosenAllergens.Count == 0)
                        continue;

                    Ui RemoveList = new("Remove allergen", chosenAllergens.ToArray());
                    int removeIndex = RemoveList.Run();
                    chosenAllergens.RemoveAt(removeIndex);
                    continue;
                }
                if (!chosenAllergens.Contains(choice))
                {
                    chosenAllergens.Add(choice);
                }
            }
        }


        // now we need to see which menus there available 
        // im very lucky cuzz we can use kevin's MenuLogic methods to:
        // - GetAllMenus();
        // - GetAllMenuItems

        List<MenuModel> AllMenus = MenuLogic.GetAllMenus();
        List<string> AllMenuOptions = [];

        foreach (MenuModel Menu in AllMenus)
        {
            AllMenuOptions.Add(Menu.MenuName);
        }
        AllMenuOptions.Add("Back");

        Ui MenuList = new Ui("Select menu", AllMenuOptions.ToArray());
        int MenuSelected = MenuList.Run();

        if (AllMenuOptions[MenuSelected] == "Back")
        {
            Start(account, pickedReservation);
            return;
        }

        MenuModel SelectedMenu = AllMenus[MenuSelected];

        List<MenuModel> AllMenuItems = MenuLogic.GetAllMenuItems();
        List<MenuModel> ItemsInMenu = [];

        foreach (MenuModel item in AllMenuItems)
            if (item.MenuName == SelectedMenu.MenuName)
            {
                ItemsInMenu.Add(item);
            }

        List<string> CategoryOptions = [];

        foreach (MenuModel item in ItemsInMenu)
            if (!CategoryOptions.Contains(item.FoodCategory))
                CategoryOptions.Add(item.FoodCategory);

        CategoryOptions.Add("Back");

        Ui CategoryList = new Ui("Select category", CategoryOptions.ToArray());
        int SelectedCategory = CategoryList.Run();

        if (CategoryOptions[SelectedCategory] == "Back")
        {
            Start(account, pickedReservation);
            return;
        }

        string selectedCategory = CategoryOptions[SelectedCategory];

        // get items in selected category
        List<MenuModel> ItemsInCategory = [];

        foreach (MenuModel item in ItemsInMenu)
            if (item.FoodCategory == selectedCategory)
                ItemsInCategory.Add(item);

        List<string> ItemOptions = [];

        foreach (MenuModel item in ItemsInCategory)
            ItemOptions.Add($"{item.Name} - €{item.Price}");

        ItemOptions.Add("Back");

        Ui ItemList = new Ui("Select item", ItemOptions.ToArray());
        int SelectedItem = ItemList.Run();

        if (ItemOptions[SelectedItem] == "Back")
        {
            Start(account, pickedReservation);
            return;
        }

        MenuModel selectedItem = ItemsInCategory[SelectedItem];
        AllSelectedItems.Add(selectedItem);

        // cannot access list before declaring !??!?
        // Need to store MenuModel selectedItem = ItemsInCategory[SelectedItem];
        // if guest wants to view order show         List<MenuModel> AllSelectedItems = [];





        Console.WriteLine("Next step");
        Console.ReadKey();

        AccountVisibility.VisibilityMenu(account);
    }
}


// 1.
// Moet een lijst aan gasten laten zien
// die nog niet in de data base staan
// als er een preorder word gemaakt

// TestGuest_1
// TestGuest_2
// TestGuest_3
// TestGuest_4
// TestGuest_5
// Etc…… 
// Edit order  is een lijst met alle gekozen items
// Confirm order 




// 1.2
// als er een gast wordt gekozen moet die eerst een lijst
// met allegieen aan geven en dan wordt het in de data base gezet


// 2.
// een guest moet aan gemaakt worden het moment
// waarop de gast zijn allergieen heeft aan gegeven
// maak for loop die een string lijst opvult 
// met guests om de nieuwe lijst met guest op te gebruiken
// om een niewe ui te maken

// maak een guest model aan om dat als type te kunnen gebruiken

// public List<>


// if guest is selected I need to make a list ui of allergens 
// a guest has to be able to select multiple ?? 


// now I need a loop to select the allergens a user has and I would like
// to highlight and dehighlight the chosen options if this is possible
// after guest is done with chosing and selectedIndex = Done
// we need to add all the highlighted options in chosenAllergens 
// and pass it to datalogic layer to make guest  

// after that we need to check if user we need to implement check cuzz
// if guest already has selected allergens they can straight up chose things 
// from menu 