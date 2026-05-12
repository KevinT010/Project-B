public class AdminAccountManagement
{
    private AccountManagementLogic _logic = new();

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("Enter the email of the user you want to manage: (or type 'back' to return)");
        string email = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(email) && email.Trim().ToLower() == "back")
        {
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
            return;
        }

        var user = _logic.GetUserByEmail(email);

        if (user == null)
        {
            Console.WriteLine("No account found with that email address.");
            Console.ReadKey();
            Start();
            return;
        }

        UserMenu(user);
    }

    private void UserMenu(AccountModel user)
    {
        string prompt = $"Managing: {user.FirstName} {user.LastName}";
        string[] options = { "Edit Account", "Delete Account", "Back" };
        Ui menu = new Ui(prompt, options);

        int choice = menu.Run();

        switch (choice)
        {
            case 0:
                EditAccount(user);
                break;
            case 1:
                DeleteAccount(user);
                break;
            case 2:
                AccountVisibility.VisibilityMenu(Session.CurrentUser);
                break;
        }
    }

    private void EditAccount(AccountModel user)
    {
        while (true)
        {
            Console.Clear();
            string prompt = $"Edit Account: {user.FirstName} {user.LastName}";
            string[] options = { "Edit first name", "Edit last name", "Edit email", "Edit phone number", "Back" };
            Ui menu = new Ui(prompt, options);

            int choice = menu.Run();

            switch (choice)
            {
                case 0:
                    Console.WriteLine("Enter new first name: (or type 'back' to return)");
                    string firstName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(firstName) && firstName.Trim().ToLower() == "back")
                    {
                        break;
                    }
                    if (_logic.FirstNameValidation(firstName) == null)
                    {
                        Console.WriteLine("First name must be between 2 and 30 characters.");
                        Console.ReadKey();
                        break;
                    }
                    user.FirstName = firstName;
                    break;

                case 1:
                    Console.WriteLine("Enter new last name: (or type 'back' to return)");
                    string lastName = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(lastName) && lastName.Trim().ToLower() == "back")
                    {
                        break;
                    }
                    if (_logic.LastNameValidation(lastName) == null)
                    {
                        Console.WriteLine("Last name must be between 2 and 30 characters.");
                        Console.ReadKey();
                        break;
                    }
                    user.LastName = lastName;
                    break;

                case 2:
                    Console.WriteLine("Enter new email: (or type 'back' to return)");
                    string email = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(email) && email.Trim().ToLower() == "back")
                    {
                        break;
                    }
                    if (_logic.EmailValidation(email) == null)
                    {
                        Console.WriteLine("Email must contain a '@' and at least one period after it.");
                        Console.ReadKey();
                        break;
                    }
                    user.Email = email;
                    break;

                case 3:
                    Console.WriteLine("Enter new phone number: (or type 'back' to return)");
                    string phoneNumber = Console.ReadLine();
                    if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Trim().ToLower() == "back")
                    {
                        break;
                    }
                    if (_logic.PhoneNumberValidation(phoneNumber) == null)
                    {
                        Console.WriteLine("Phone number must start with 0 or + and be between 5 and 15 digits.");
                        Console.ReadKey();
                        break;
                    }
                    user.PhoneNumber = phoneNumber;
                    break;

                case 4:
                    {
                        AccountVisibility.VisibilityMenu(Session.CurrentUser);
                        break;
                    }

            }
        }
    }

    private void DeleteAccount(AccountModel user)
    {
        Console.Clear();
        Console.WriteLine($"Are you sure you want to delete the account of {user.FirstName} {user.LastName}?");

        string[] options = { "Confirm Delete", "Back" };
        Ui menu = new Ui("Delete Account", options);

        int choice = menu.Run();

        switch (choice)
        {
            case 0:
                _logic.DeleteAccount(user.Id);
                Console.WriteLine("Account successfully deleted.");
                Thread.Sleep(2000);
                Start();
                break;
            case 1:
                UserMenu(user);
                break;
        }
    }
}
