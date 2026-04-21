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
        Console.Clear();
        Console.WriteLine("Edit Account");

        Console.Write("New name: ");
        string name = Console.ReadLine();

        Console.Write("New email: ");
        string email = Console.ReadLine();

        Console.Write("New phone: ");
        string phone = Console.ReadLine();

        Console.Write("New password: ");
        string password = Console.ReadLine();

        Console.WriteLine("Save changes? (yes/no)");
        string confirm = Console.ReadLine();

        if (!confirm.Equals("yes", StringComparison.OrdinalIgnoreCase))
            return;

        var user = Session.CurrentUser;

        if (!string.IsNullOrWhiteSpace(name))
            user.FullName = name;

        if (!string.IsNullOrWhiteSpace(email))
            user.Email = email;

        if (!string.IsNullOrWhiteSpace(phone))
            user.PhoneNumber = phone;

        if (!string.IsNullOrWhiteSpace(password))
            user.Password = password;

        new AccountRegistrationLogic().UpdateAccount(user);

        Console.WriteLine("Account updated successfully.");
        Console.ReadKey();
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
