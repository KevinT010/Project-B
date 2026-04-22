public class AccountRegistration
{
    public void Start()
    {
        Console.Clear();
        Console.WriteLine("Account-Registration");

        var logic = new AccountRegistrationLogic();

        string firstName = string.Empty;
        string lastName = string.Empty;
        string email = string.Empty;
        string phoneNumber = string.Empty;
        string password = string.Empty;

        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your firstname: (or type 'back' to return to the main menu)");
            firstName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(firstName) && firstName.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.FirstNameValidation(firstName);
            if (!isValid)
            {
                Console.WriteLine("Firstname must be between 2 and 30 characters.");
            }
        }

        isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your lastname: (or type 'back' to return to the main menu)");
            lastName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(lastName) && lastName.Trim().ToLower() == "back")
            {
                StartMenu.Start();
            }

            isValid = logic.LastNameValidation(lastName);
            if (!isValid)
            {
                Console.WriteLine("Lastname must be between 2 and 30 characters.");
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
            password = Console.ReadLine();

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
            StartMenu.Start();
        }
        else
        {
            Console.WriteLine("Account registration failed. Please try again.");
        }
    }
}