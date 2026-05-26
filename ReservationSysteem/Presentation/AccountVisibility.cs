public static class AccountVisibility
{
    public static void VisibilityMenu(AccountModel account)
    {
        if (account.AccountLevel == 2)
        {
            ShowAdminMenu();
        }
        else
        {
            ShowUserMenu();
        }
    }

    private static void ShowUserMenu()
    {
        string prompt = $"Welcome {Session.CurrentUser.FirstName} {Session.CurrentUser.LastName}!\nUser dashboard";
        string[] options = { "Menu", "Reservations", "Floor-plan", "Account management","Operating Hours", "Reviews", "Logout" };
        Ui userMenu = new Ui(prompt, options);
        int selectedIndex = userMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Menu menu = new Menu();
                menu.Start();
                break;
            case 1:
                string reservation_prompt = "Reservations";
                string[] reservationOptions = { "Make a reservation", "View reservations", "Return" };
                Ui reservationMenu = new(reservation_prompt, reservationOptions);
                int reservationChoice = reservationMenu.Run();

                switch (reservationChoice)
                {
                    case 0:
                        Reservation reservation = new();
                        reservation.Start(Session.CurrentUser);
                        break;
                    case 1:
                        ViewReservations viewReservations = new();
                        viewReservations.Start(Session.CurrentUser);
                        break;
                    case 2:
                        ShowUserMenu();
                        break;
                }
                break;
            case 2:
                TableMap.DisplayStatic();
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                ShowUserMenu();
                break;
            case 3:
                AccountManagement accountManagement = new AccountManagement();
                accountManagement.Start();
                break;
            case 4:
                OperatingHour operatingHour = new OperatingHour();
                operatingHour.Start();
                break;
            case 5: 
                Review review = new Review();
                review.Start(Session.CurrentUser);
                break;
            case 6:
                Session.Logout();
                break;
        }
    }

    private static void ShowAdminMenu()
    {
        string prompt = $"Welcome {Session.CurrentUser.FirstName} {Session.CurrentUser.LastName}!\nAdmin dashboard";
        string[] options = { "Show all reservations", "Change menu", "Menu", "Floor-plan", "Admin Account Management", "Reviews", "Logout" };
        Ui adminMenu = new Ui(prompt, options);
        int selectedIndex = adminMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                ViewReservations viewReservations = new();
                viewReservations.Start(Session.CurrentUser);
                break;
            case 1:
                ManageMenu manageMenu = new ManageMenu();
                manageMenu.Start();
                break;

            case 2:
                Menu menu = new Menu();
                menu.Start();
                break;
            case 3:
                TableMap.DisplayStatic();
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                ShowAdminMenu();
                break;
            case 4:
                AdminAccountManagement adminAccountManagement = new AdminAccountManagement();
                adminAccountManagement.Start();
                break;
            case 5:
                Review review = new Review();
                review.Start(Session.CurrentUser);
                break;
            case 6:
                Console.Clear();
                Session.Logout();
                break;
        }
    }
}