public class PreOrder
{
    public void Start(AccountModel account)
    {
        // account null check
        if (account == null)
            return;

        PreOrderLogic logic = new PreOrderLogic();
        var reservations = logic.GetReservations(account.Id);

        // List null check
        if (reservations.Count == 0)
        {
            Console.WriteLine("No reservations found.");
            Console.ReadKey();
            AccountVisibility.VisibilityMenu(account);
            return;
        }

        List<string> options = new List<string>();

        foreach (var r in reservations)
        {
            options.Add($"{r.DateTime:dd-MM-yyyy HH:mm} | Guests: {r.NumberOfGuests}");
        }

        options.Add("Back");

        Ui ReservationList = new Ui("Select a reservation for Pre-Order", options.ToArray());

        int selectedIndex = ReservationList.Run();

        if (options[selectedIndex] == "Back")
        {
            AccountVisibility.VisibilityMenu(account);
            return;
        }

        ReservationModel selectedReservation = reservations[selectedIndex];

        Console.Clear();
        Console.WriteLine($"Selected reservation:");
        Console.WriteLine($"{selectedReservation.DateTime} - {selectedReservation.NumberOfGuests} guests");

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


        Console.WriteLine("\nNext step");
        Console.ReadKey();

        AccountVisibility.VisibilityMenu(account);
    }
}