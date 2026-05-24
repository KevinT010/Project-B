public class ViewReservations
{
    public void Start(AccountModel account)
    {
        ReservationLogic reservationLogic = new ReservationLogic();
        if (account.AccountLevel == 1)
        {
            var reservations = reservationLogic.GetReservationsByAccountId(account.Id);

            if (reservations.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("You have no reservations.");
                Thread.Sleep(2000);
                AccountVisibility.VisibilityMenu(account);
                return;
            }

            string[] options = new string[reservations.Count + 1];
            for (int i = 0; i < reservations.Count; i++)
            {
                string status = reservationLogic.IsExpired(reservations[i]) ? "[Expired]" : "[Active]";
                options[i] = $"{status} {reservations[i].DateTime:dd-MM-yyyy HH:mm} -{reservations[i].DateTime.AddHours(2): HH:mm} | Adults: {reservations[i].NumberOfGuests - reservations[i].NumberOfKids} | Kids: {reservations[i].NumberOfKids} | Kids in play area: {reservations[i].KidsPlayArea}";
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

        else
        {
            var days = reservationLogic.GetDays();

            if (days.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("There are no reservations.");
                Thread.Sleep(2000);
                AccountVisibility.VisibilityMenu(account);
                return;
            }

            string[] dayOptions = new string[days.Count + 1];
            for (int i = 0; i < days.Count; i++)
            {
                var reservationsOnDay = reservationLogic.GetReservationsByDay(days[i]);
                dayOptions[i] = $"{days[i]:dd-MM-yyyy} | {reservationsOnDay.Count} reservation(s)";
            }
            dayOptions[days.Count] = "Go back";

            Ui dayList = new Ui("Select a day", dayOptions);
            int selectedDayIndex = dayList.Run();

            if (selectedDayIndex == days.Count)
            {
                AccountVisibility.VisibilityMenu(account);
                return;
            }

            var reservations = reservationLogic.GetReservationsByDay(days[selectedDayIndex]);

            string[] options = new string[reservations.Count + 1];
            for (int i = 0; i < reservations.Count; i++)
            {
                string status = reservationLogic.IsExpired(reservations[i]) ? "[Expired]" : "[Active]";
                options[i] = $"{status} Reserved by: {reservations[i].FirstName} {reservations[i].LastName} | {reservations[i].DateTime:HH:mm}-{reservations[i].DateTime.AddHours(2):HH:mm} | Adults: {reservations[i].NumberOfGuests - reservations[i].NumberOfKids} | Kids: {reservations[i].NumberOfKids} | Kids in play area: {reservations[i].KidsPlayArea}";
            }
            options[reservations.Count] = "Go back";

            Ui reservationList = new Ui($"Reservations on {days[selectedDayIndex]:dd-MM-yyyy}", options);
            int selectedIndex = reservationList.Run();

            if (selectedIndex == reservations.Count)
            {
                Start(account);
                return;
            }

            string[] reservationActions = { "Go back" };
            Ui actionMenu = new Ui("What would you like to do?", reservationActions);
            actionMenu.Run();
            Start(account);
        }
    }
}