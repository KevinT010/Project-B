public static class StartMenu
{
    public static void Start()
    {
        string prompt = "Welcome to Reservation System";
        string[] options = { "Account Registration", "Account Login", "Menu's", "Exit" };
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
                Console.WriteLine("Thank you for using the reservation system");
                Environment.Exit(0);
                break;
        }
    }
}