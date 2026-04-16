using BCrypt.Net;

public class AccountRegistrationLogic
{
    private AccountRegistrationAccess _access = new();
    public bool FirstNameValidation(string firstName)
    {
        if (firstName.Length < 2 || firstName.Length > 30)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool LastNameValidation(string lastName)

    {
        if (lastName.Length < 2 || lastName.Length > 30)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public bool EmailValidation(string email)
    {
        int atIndex = email.IndexOf("@");
        int dotIndex = email.LastIndexOf(".");
        
        if (atIndex > 0 && dotIndex > atIndex && _access.GetByEmail(email) == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PhoneNumberValidation(string phoneNumber)
    {
        if (phoneNumber.StartsWith("0") || phoneNumber.StartsWith("+") || phoneNumber.StartsWith("+353") && phoneNumber.Length >= 5 && phoneNumber.Length <= 15)
        {
            try
            {
              Convert.ToInt64(phoneNumber);
            }
            catch(FormatException)
            {
                return false;
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool PasswordValidation(string password)
    {
        if (password.Length < 8 || password.Length > 20)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public bool AccountRegistrationValidation(string firstName, string lastName, string email, string phoneNumber, string password)
    {
        if (FirstNameValidation(firstName) && LastNameValidation(lastName) && EmailValidation(email) && PhoneNumberValidation(phoneNumber) && PasswordValidation(password))
        {
            _access.InsertAccount(new AccountModel(firstName, lastName, email, phoneNumber, BCrypt.Net.BCrypt.HashPassword(password), 1));
            return true;
        }
        else
        {
            return false;
        }
    }


}