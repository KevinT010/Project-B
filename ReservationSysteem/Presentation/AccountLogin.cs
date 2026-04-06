public class AccountLogin
{
  protected string ValidateInput(string text, string errorMessage, Func<string, bool> validationFunction)
    {
        while (true)
        {
            Console.WriteLine(text);
            string input = Console.ReadLine();

            if (validationFunction(input))
            {
                return input;
            }

            Console.WriteLine(errorMessage);
        }
    }

    public void Start()
    {
        Console.WriteLine("Account-Login");

        var logic = new AccountLoginLogic();

        string email = ValidateInput("Enter your email:", "Email must contain a @ or email is not registered.", logic.EmailValidation);

        string password = ValidateInput("Enter your password:", "Password must be between 8 and 20 characters.", logic.PasswordValidation);

        while (logic.AccountLoginValidation(email, password) == null)
        {
            Console.WriteLine("wrong email or password. Please try again.");
            email = ValidateInput("Enter your email:", "Email must contain a @ or email is not registered.", logic.EmailValidation);
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