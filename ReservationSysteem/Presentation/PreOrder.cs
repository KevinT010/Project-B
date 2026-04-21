public class PreOrder
{
    public void Start(AccountModel account)
    {
        // account null check
        if (account == null)
        {
            return;
        }
        
        PreOrderLogic logic = new PreOrderLogic();
        var reservations = logic.GetReservations(account.Id);

        // List null check
        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            Console.ReadKey();
            return;
        }

        List<string> options = new List<string>();

        foreach (var reservation in reservations)
            options.Add($"{reservation.DateTime:dd-MM-yyyy HH:mm} | Guests: {reservation.NumberOfGuests}");

        options.Add("Back");

        Ui ReservationList = new Ui("Select a reservation for Pre-Order", options.ToArray());
        int selectedIndexReservation = ReservationList.Run();

        if (options[selectedIndexReservation] == "Back")
            return;

        ReservationModel selectedReservation = reservations[selectedIndexReservation];

        Console.Clear();
        Console.WriteLine($"Selected reservation:");
        Console.WriteLine($"{selectedReservation.DateTime} - {selectedReservation.NumberOfGuests} guests");

        // ------------------------------------------------------------------------------------------------------------------------------------------- 
        List<string> Guest = new();
        int GuestCounter = 1;

        for (int i = 0; i <= selectedReservation.NumberOfGuests; i++)
        {
            Guest.Add($"Guest {GuestCounter}");
            GuestCounter++;
        }

        Guest.Add("Edit Order");
        Guest.Add("Back");

        Ui GuestList = new Ui("Select Guest", Guest.ToArray());
        int selectedIndexGuest = GuestList.Run();

        if (Guest[selectedIndexGuest] == "Back")
        {
            Start(account);
            return;
        }

        if (Guest[selectedIndexGuest] == "Edit Order")
        {
            // Edit order  is een lijst met alle gekozen items
            // Confirm order
            return;
        }
        // -------------------------------------------------------------------------------------------------------------------------------------------

        // if guest is selected I need to make a list ui of allergens 
        // a guest has to be able to select multiple ?? 

        List<string> allergenOptions = new() { "Milk / Dairy", "Egg", "Shellfish", "Fish", "Peanuts / Nuts", "Wheat / Gluten", "Soy", "Sesame", "Alcohol", "None", "Remove item", "Back", "Done" };
        List<string> chosenAllergens = new();

        // now I need a loop to select the allergens a user has and I would like
        // to highlight and dehighlight the chosen options if this is possible
        // after guest is done with chosing and selectedIndex = Done
        // we need to add all the highlighted options in chosenAllergens 
        // and pass it to datalogic layer to make guest  

        // after that we need to check if user we need to implement check cuzz
        // if guest already has selected allergens they can straight up chose things 
        // from menu 

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
                Start(account);
                return;
            }

            if (choice == "Done")
            {
                // if user is done grab list chosenAllergens and send it too
                // datalogic layer to make user and continue making the pre-order flow
                // 
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
                    continue;

                Ui RemoveList = new("Remove allergen", chosenAllergens.ToArray());
                int removeIndex = RemoveList.Run();
                chosenAllergens.RemoveAt(removeIndex);
                continue;
            }
            if (!chosenAllergens.Contains(choice))
                chosenAllergens.Add(choice);
        }

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