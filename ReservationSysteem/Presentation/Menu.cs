public static class Menu
{
    public static void Start()
    {
        string prompt = "Welcome to Reservation System";
        string[] options = { "Account Registration", "Account Login", "Exit" };
        Ui Menu = new Ui(prompt, options);
        int selectedIndex = Menu.Run();

        if(selectedIndex == 0)
        {
            AccountRegistration accountRegistration = new AccountRegistration();
            accountRegistration.Start();
        }
        else if(selectedIndex == 1)
        {
            Console.WriteLine("Account Login");
            AccountRegistration accountRegistration = new AccountRegistration();
            accountRegistration.Start();
        }
    }
}