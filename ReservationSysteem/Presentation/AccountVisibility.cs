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
        string prompt = "User Dashboard Welcome " + Session.CurrentUser.FullName;
        string[] options = { "Menu", " Place reservations", "Floor-plan", "Account management", "Logout" };
        Ui userMenu = new Ui(prompt, options);
        int selectedIndex = userMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                break;
            case 1:
                Menu menu = new Menu();
                menu.Start();
                break;
            case 2:
                Reservation reservation = new();
                    reservation.Start(Session.CurrentUser);
                    break;
            case 3:
                break;
            case 4:
                break;
        }
    }

    private static void ShowAdminMenu()
    {
        string prompt = "Admin Dashboard Welcome " + Session.CurrentUser.FullName;
        string[] options = { "Show all reservations", "Change menu", "Menu", "Floor-plan","Account management" , "Logout" };
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
                break;
        }
    }
}