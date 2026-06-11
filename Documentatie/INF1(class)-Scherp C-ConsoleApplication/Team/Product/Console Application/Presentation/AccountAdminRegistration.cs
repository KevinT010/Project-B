public class AccountAdminRegistration
{
    private new AccountAdminRegistrationLogic logic = new AccountAdminRegistrationLogic();
     public void Start()
    {
        Console.Clear();
        Console.WriteLine("Account registration");

        string firstName = string.Empty;
        string lastName = string.Empty;
        string email = string.Empty;
        string phoneNumber = string.Empty;
        string password = string.Empty;



        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your first name: (or type 'back' to return to the main menu)");
            firstName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(firstName) && firstName.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.FirstNameValidation(firstName);
            if (!isValid)
            {
                Console.WriteLine("First name must be between 2 and 30 characters.");
            }
        }

        isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your last name: (or type 'back' to return to the main menu)");
            lastName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(lastName) && lastName.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.LastNameValidation(lastName);
            if (!isValid)
            {
                Console.WriteLine("Last name must be between 2 and 30 characters.");
            }
        }

        isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your email: (or type 'back' to return to the main menu)");
            email = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(email) && email.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.EmailValidation(email);
            if (!isValid)
            {
                Console.WriteLine("Email must contain a @ and at least one period(.) after the @, or the email is already registered.");
            }
        }

        isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your phonenumber: (or type 'back' to return to the main menu)");
            phoneNumber = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.PhoneNumberValidation(phoneNumber);
            if (!isValid)
            {
                Console.WriteLine("Phonenumber must start with 0 or + and must only contain numbers and between 5 and 15 characters.");
            }
        }

        isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your password (password must be between 8 and 20 characters): (or type 'back' to return to the main menu)");
            password = ReadPassword();

            if (!string.IsNullOrWhiteSpace(password) && password.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.PasswordValidation(password);
            if (!isValid)
            {
                Console.WriteLine("Password must be between 8 and 20 characters.");
            }
        }

        if (logic.AccountRegistrationValidation(firstName, lastName, email, phoneNumber, password))
        {
            Console.WriteLine("Account successfully registered.");
            Thread.Sleep(2000);
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
        }
        else
        {
            Console.WriteLine("Account registration failed. Please try again.");
        }
    }

    private string ReadPassword()
    {
        string password = string.Empty;
        ConsoleKey key;

        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && password.Length > 0)
            {
                Console.Write("\b \b");
                password = password[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write("*");
                password += keyInfo.KeyChar;
            }
        } 
        while (key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }
}