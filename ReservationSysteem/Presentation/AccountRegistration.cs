public class AccountRegistration
{
    private string ValidateInput(string text, string errorMessage, Func<string, bool> validationFunction)
    {
        while (true)
        {
            Console.WriteLine($"{text} (or type 'back' to return to the main menu)");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input) && input.Trim().ToLower() == "back")
            {
                return null;
            }

            if (validationFunction(input))
            {
                return input;
            }

            Console.WriteLine(errorMessage);
        }
    }

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("Account-Registration");

        var logic = new AccountRegistrationLogic();

        string fullName = ValidateInput("Enter your fullname:", "FullName must be between 2 and 30 characters.", logic.FullNameValidation);
        if (fullName == null)
        {
            StartMenu.Start();
        }
        string email = ValidateInput("Enter your email:", "Email must contain a @ and at least one period(.) after the @, or the email is already registered.", logic.EmailValidation);
        if (email == null)
        {
            StartMenu.Start();
        }

        string phoneNumber = ValidateInput("Enter your phonenumber (Phonenumber must be between 5 and 15 characters):", "Phonenumber must start with 0 or +353 or + and must only contain numbers and between 5 and 15 characters.", logic.PhoneNumberValidation);
        if (phoneNumber == null)
        {
            StartMenu.Start();
        }

        string password = ValidateInput("Enter your password (password must be between 8 and 20 characters):", "Password must be between 8 and 20 characters.", logic.PasswordValidation);
        if (password == null)
        {
            StartMenu.Start();
        }

        if (logic.AccountRegistrationValidation(fullName, email, phoneNumber, password))
        {
            Console.WriteLine("Account successfully registered.");
            Thread.Sleep(2000);
            StartMenu.Start();
        }
        else
        {
            Console.WriteLine("Account registration failed. Please try again.");
            StartMenu.Start();
        }
    }
}