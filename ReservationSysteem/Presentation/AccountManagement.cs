using BCrypt.Net;

public class AccountManagement
{
    private AccountManagementLogic _logic = new();

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
                AccountVisibility.VisibilityMenu(Session.CurrentUser);
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
                "Edit First Name",
                "Edit Last Name",
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
                    bool isValidFirstName = false;
                    while (!isValidFirstName)
                    {
                        Console.WriteLine("Enter your new firstname: (or type 'back' to return)");
                        string firstName = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(firstName) && firstName.Trim().ToLower() == "back")
                        {
                            break;
                        }

                        isValidFirstName = _logic.FirstNameValidation(firstName) != null;
                        if (!isValidFirstName)
                        {
                            Console.WriteLine("Firstname must be between 2 and 30 characters.");
                        }
                        else
                        {
                            user.FirstName = firstName;
                        }
                    }
                    break;

                case 1:
                    bool isValidLastName = false;
                    while (!isValidLastName)
                    {
                        Console.WriteLine("Enter your new lastname: (or type 'back' to return)");
                        string lastName = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(lastName) && lastName.Trim().ToLower() == "back")
                        {
                            break;
                        }

                        isValidLastName = _logic.LastNameValidation(lastName) != null;
                        if (!isValidLastName)
                        {
                            Console.WriteLine("Lastname must be between 2 and 30 characters.");
                        }
                        else
                        {
                            user.LastName = lastName;
                        }
                    }
                    break;

                case 2:
                    bool isValidEmail = false;
                    while (!isValidEmail)
                    {
                        Console.WriteLine("Enter your new email: (or type 'back' to return)");
                        string email = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(email) && email.Trim().ToLower() == "back")
                        {
                            break;
                        }

                        isValidEmail = _logic.EmailValidation(email) != null;
                        if (!isValidEmail)
                        {
                            Console.WriteLine("Email must contain a @ and at least one period(.) after the @, or the email is not registered.");
                        }
                        else
                        {
                            user.Email = email;
                        }
                    }
                    break;

                case 3:
                    bool isValidPhone = false;
                    while (!isValidPhone)
                    {
                        Console.WriteLine("Enter your new phonenumber: (or type 'back' to return)");
                        string phoneNumber = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Trim().ToLower() == "back")
                        {
                            break;
                        }

                        isValidPhone = _logic.PhoneNumberValidation(phoneNumber) != null;
                        if (!isValidPhone)
                        {
                            Console.WriteLine("Phonenumber must start with 0 or +353 or + and must only contain numbers and between 5 and 15 characters.");
                        }
                        else
                        {
                            user.PhoneNumber = phoneNumber;
                        }
                    }
                    break;

                case 4:
                    bool isValidPassword = false;
                    while (!isValidPassword)
                    {
                        Console.WriteLine("Enter your new password (password must be between 8 and 20 characters): (or type 'back' to return)");
                        string password = Console.ReadLine();

                        if (!string.IsNullOrWhiteSpace(password) && password.Trim().ToLower() == "back")
                        {
                            break;
                        }

                        isValidPassword = _logic.PasswordValidation(password) != null;
                        if (!isValidPassword)
                        {
                            Console.WriteLine("Password must be between 8 and 20 characters.");
                        }
                        else
                        {
                            user.Password = BCrypt.Net.BCrypt.HashPassword(password);
                        }
                    }
                    break;

                case 5:
                    if (_logic.UpdateAccount(user))
                    {
                        Console.WriteLine("Account updated successfully.");
                        Thread.Sleep(2000);
                        AccountManagement accountManagement = new AccountManagement();
                        accountManagement.Start();
                    }
                    else
                    {
                        Console.WriteLine("Update failed. Check your inputs.");
                        AccountManagement accountManagement = new AccountManagement();
                        accountManagement.Start();
                    }
                    Console.ReadKey();
                    return;
            }
        }
    }

    private void DeleteAccount()
    {
        Console.Clear();
        Console.WriteLine("Enter your password to confirm deletion:");
        string password = Console.ReadLine();

        var user = Session.CurrentUser;

        if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            Console.WriteLine("Incorrect password.");
            Console.ReadKey();
            return;
        }

        _logic.DeleteAccount(user.Id);

        Session.CurrentUser = null;
        Console.WriteLine("Account deleted successfully.");
        Thread.Sleep(2000);
        StartMenu.Start();
    }
}