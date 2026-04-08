public class AccountLogin
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
        Console.WriteLine("Account-Login");

        var logic = new AccountLoginLogic();

        string email = ValidateInput("Enter your email:", "Email must contain a @ and a period(.) or email is not registered.", logic.EmailValidation);
        if (email == null)
        {
            StartMenu.Start();
        }

        string password = ValidateInput("Enter your password:", "Password must be between 8 and 20 characters.", logic.PasswordValidation);
        if (password == null)
        {
            StartMenu.Start();
        }

        while (logic.AccountLoginValidation(email, password) == null)
        {
            Console.WriteLine("wrong email or password. Please try again.");
            email = ValidateInput("Enter your email:", "Email must contain a @ and a period(.) or email is not registered.", logic.EmailValidation);
            password = ValidateInput("Enter your password:", "Password must be between 8 and 20 characters.", logic.PasswordValidation);
        }

        var loggedInUser = logic.AccountLoginValidation(email, password);

        if (logic.AccountLoginValidation(email, password) != null)
        {
            Session.CurrentUser = loggedInUser;
            Console.WriteLine("Account successfully logged in.");
            int waitTime = 2000;
            Thread.Sleep(waitTime);
            StartMenu.Start();

        }
        else
        {
            Console.WriteLine("Account login failed. Please try again.");
            StartMenu.Start();
        }

    }



}