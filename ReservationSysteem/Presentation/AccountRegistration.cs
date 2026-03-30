public class AccountRegistration
{

     private string ValidateInput(string text, string errorMessage, Func<string, bool> validationFunction)
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
        Console.WriteLine("Account-Registration");

        var logic = new AccountRegistrationLogic();

        string fullName = ValidateInput("Enter your fullname:", "FullName must be between 2 and 30 characters.", logic.FullNameValidation);

        string email = ValidateInput( "Enter your email:", "Email must contain a @ or email is already registered.", logic.EmailValidation);

        string phoneNumber = ValidateInput( "Enter your phonenumber:", "Phonenumber must start with 06 or +31", logic.PhoneNumberValidation);

        string password = ValidateInput( "Enter your password:",  "Password must be between 8 and 20 characters.",  logic.PasswordValidation);

        if (logic.AccountRegistrationValidation(fullName, email, phoneNumber, password))
        {
            Console.WriteLine("Account successfully registered.");
        }
        else
        {
            Console.WriteLine("Account registration failed. Please try again.");
            Menu.Start();
        }
        
    }



    
}