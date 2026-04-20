public class AccountLogin
{
    public void Start()
    {
        Console.Clear();
        Console.WriteLine("Account-Login");

        var logic = new AccountLoginLogic();

        string email = string.Empty;
        string password = string.Empty;

        bool isValid = false;

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
                Console.WriteLine("Email must contain a @ and at least one period(.) after the @, or the email is not registered.");
            }
        }

        isValid = false;

        while (!isValid)
        {
            Console.WriteLine("Enter your password: (or type 'back' to return to the main menu)");
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

        while (logic.AccountLoginValidation(email, password) == null)
        {
            Console.WriteLine("wrong email or password. Please try again.");
            
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
                    Console.WriteLine("Email must contain a @ and at least one period(.) after the @, or the email is not registered.");
                }
            }

            isValid = false;

            while (!isValid)
            {
                Console.WriteLine("Enter your password: (or type 'back' to return to the main menu)");
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
        }

        var loggedInUser = logic.AccountLoginValidation(email, password);

        if (loggedInUser != null)
        {
            Session.CurrentUser = loggedInUser;
            Console.WriteLine("Account successfully logged in.");
            int waitTime = 2000;
            Thread.Sleep(waitTime);
            AccountVisibility.VisibilityMenu(loggedInUser);
        }
        else
        {
            Console.WriteLine("Account login failed. Please try again.");
            StartMenu.Start();
        }
    }
}