public static class StartMenu
{
    public static void Start()
    {
        string prompt = "A chinese family restaurant reservation system";
        string[] options = { "Account Registration", "Account Login", "Menu's", "Operating Hours", "Exit" };
        Ui StartMenu = new Ui(prompt, options);
        int selectedIndex = StartMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                AccountRegistration accountRegistration = new AccountRegistration();
                accountRegistration.Start();
                break;
            case 1:
                AccountLogin accountLogin = new AccountLogin();
                accountLogin.Start();
                break;
            case 2:
                Menu menu = new Menu();
                menu.Start();
                break;
            case 3:
                OperatingHour operatingHour = new OperatingHour();
                operatingHour.Start();
                break; 
            case 4:
                AdminAccountManagement adminAccountManagement = new AdminAccountManagement();
                adminAccountManagement.Start();
                break; 
            case 5:
                Console.WriteLine("Thank you for using the reservation system");
                Environment.Exit(0);
                break;
        }
    }
}