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
        string prompt = $"User Dashboard Welcome {Session.CurrentUser.FirstName} {Session.CurrentUser.LastName}";
        string[] options = { "Menu", "Reservations", "Floor-plan", "Account management", "Logout" };
        Ui userMenu = new Ui(prompt, options);
        int selectedIndex = userMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Menu menu = new Menu();
                menu.Start();
                break;
            case 1:
                Reservation reservation = new();
                reservation.Start(Session.CurrentUser);
                break;
            case 2:
                TableMap.DisplayStatic();
                Console.WriteLine("\nPress any key to return...");
                Console.ReadKey();
                ShowUserMenu();
                break;
            case 3:
                break;
            case 4:
                Session.Logout();
                break;
        }
    }

    private static void ShowAdminMenu()
    {
        string prompt = $"Admin Dashboard Welcome {Session.CurrentUser.FirstName} {Session.CurrentUser.LastName}";
        string[] options = { "Show all reservations", "Change menu", "Menu", "Floor-plan", "Account management", "Logout" };
        Ui adminMenu = new Ui(prompt, options);
        int selectedIndex = adminMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                Menu menu = new Menu();
                menu.Start();
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                Console.Clear();
                Session.Logout();
                break;
        }
    }
}