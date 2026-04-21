public class ViewReservations
{
    public void Start(AccountModel account)
    {
        ReservationLogic reservationLogic = new ReservationLogic();
        var reservations = reservationLogic.GetReservationsByAccountId(account.Id);

        if (reservations.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("You have no reservations.");
            Thread.Sleep(2000);
            AccountVisibility.VisibilityMenu(account);
            return;
        }

        string[] options = new string [reservations.Count + 1];
        for (int i = 0; i < reservations.Count; i++)
        {
            string status = reservationLogic.IsExpired(reservations[i]) ? "[Expired]" : "[Active]";
            options[i] = $"{status} {reservations[i].DateTime:dd-MM-yyyy HH:mm} -{reservations[i].DateTime.AddHours(2): HH:mm} | Adults: {reservations[i].NumberOfGuests - reservations[i].NumberOfKids} | Kids: {reservations[i].NumberOfKids}";
        }
        options[reservations.Count] = "Go back";

        Ui reservationList = new Ui("Your reservations", options);
        int selectedIndex = reservationList.Run();

        if (selectedIndex == reservations.Count)
        {
            AccountVisibility.VisibilityMenu(account);
            return;
        }

        ReservationModel pickedReservation = reservations[selectedIndex];

        if (reservationLogic.IsExpired(pickedReservation))
        {
            Console.Clear();
            Console.WriteLine("This reservation has expired, you cannot pre-order for it.");
            Thread.Sleep(2000);
            Start(account);
            return;
        }

        string[] reservationActions = { "Pre-Order", "Go back" };
        Ui actionMenu = new Ui("What would you like to do?", reservationActions);
        int actionIndex = actionMenu.Run();

        switch (actionIndex)
        {
            case 0:
                PreOrder preOrder = new();
                preOrder.Start(account, pickedReservation);
                break;
            case 1:
                Start(account);
                break;
        }
    }
}