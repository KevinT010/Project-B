public class PreOrder
{
    public void Start(AccountModel account)
    {
        // account null check
        if (account == null)
            return;

        PreOrderLogic logic = new PreOrderLogic();
        ReservationLogic reservationlogic = new ReservationLogic();
        var reservations = logic.GetReservations(account.Id);
        reservations = reservationlogic.GetActiveReservations(reservations);

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
            options.Add($"{r.DateTime:dd-MM-yyyy HH:mm} | Adults: {r.NumberOfGuests} | Kids: {r.NumberOfKids}");
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

        Console.WriteLine("\nNext step");
        Console.ReadKey();

        AccountVisibility.VisibilityMenu(account);
    }
}