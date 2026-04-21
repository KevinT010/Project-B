public class CustomerManagement
{
    public void Start()
    {
        if (Session.CurrentUser == null)
        {
            Console.WriteLine("You must be logged in.");
            Console.ReadKey();
            StartMenu.Start();
            return;
        }

        string prompt = "Account Management";
        string[] options = { "Edit Account", "Delete Account", "Back" };
        Ui menu = new Ui(prompt, options);

        int choice = menu.Run();

        switch (choice)
        {
            case 0:
                EditAccount();
                break;

            case 1:
                string deletePrompt = "Delete Account";
                string[] deleteOptions = { "Confirm Delete", "Back" };
                Ui deleteMenu = new Ui(deletePrompt, deleteOptions);

                int deleteChoice = deleteMenu.Run();

                switch (deleteChoice)
                {
                    case 0:
                        DeleteAccount();
                        break;

                    case 1:
                        Start();
                        break;
                }
                break;

            case 2:
                string backPrompt = "Back Menu";
                string[] backOptions = { "Go to Visibility Menu", "Go to Start Menu" };
                Ui backMenu = new Ui(backPrompt, backOptions);

                int backChoice = backMenu.Run();

                switch (backChoice)
                {
                    case 0:
                        AccountVisibility.VisibilityMenu(Session.CurrentUser);
                        break;

                    case 1:
                        StartMenu.Start();
                        break;
                }
                break;
        }
    }

private void EditAccount()
{
    while (true)
    {
        Console.Clear();
        string prompt = "Edit Account";
        string[] options = 
        { 
            "Edit Name", 
            "Edit Email", 
            "Edit Phone", 
            "Edit Password", 
            "Save & Back" 
        };

        Ui menu = new Ui(prompt, options);
        int choice = menu.Run();

        var user = Session.CurrentUser;

        switch (choice)
        {
            case 0:
                Console.Write("New name: ");
                string name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                    user.FullName = name;
                break;

            case 1:
                Console.Write("New email: ");
                string email = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(email))
                    user.Email = email;
                break;

            case 2:
                Console.Write("New phone: ");
                string phone = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(phone))
                    user.PhoneNumber = phone;
                break;

            case 3:
                Console.Write("New password: ");
                string password = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(password))
                    user.Password = password;
                break;

            case 4:
                new AccountRegistrationLogic().UpdateAccount(user);
                Console.WriteLine("Account updated successfully.");
                Console.ReadKey();
                return;
        }
    }
}

    private void DeleteAccount()
    {
        Console.Clear();
        Console.WriteLine("Delete Account");
        Console.WriteLine("Are you sure? (yes/no)");

        string confirm = Console.ReadLine();

        if (!confirm.Equals("yes", StringComparison.OrdinalIgnoreCase))
            return;

        Console.Write("Enter your password: ");
        string password = Console.ReadLine();

        var user = Session.CurrentUser;

        if (user.Password != password)
        {
            Console.WriteLine("Incorrect password.");
            Console.ReadKey();
            return;
        }

        new ReservationLogic().DeleteReservationsByUser(user.Id);
        new AccountRegistrationLogic().DeleteAccount(user.Id);

        Session.CurrentUser = null;

        Console.WriteLine("Account deleted successfully.");
        Console.ReadKey();

        StartMenu.Start();
    }
}
