using BCrypt.Net;

public class AccountRegistrationLogic
{
    private AccountRegistrationAccess _access = new();
    public bool FullNameValidation(string fullName)
    {
        if (fullName.Length < 2 || fullName.Length > 30)
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
        if (phoneNumber.StartsWith("0") || phoneNumber.StartsWith("+") || phoneNumber.StartsWith("+353"))
        {
            try
            {
              Convert.ToInt32(phoneNumber);
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


    public bool AccountRegistrationValidation(string fullName, string email, string phoneNumber, string password)
    {
        if (FullNameValidation(fullName) && EmailValidation(email) && PhoneNumberValidation(phoneNumber) && PasswordValidation(password))
        {
            _access.InsertAccount(new AccountModel(fullName, email, phoneNumber, BCrypt.Net.BCrypt.HashPassword(password), 1));
            return true;
        }
        else
        {
            return false;
        }
    }


}