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
        if (email.Contains("@") && _access.GetByEmail(email) == null)
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
        if (phoneNumber.StartsWith("06") || phoneNumber.StartsWith("+31"))
        {
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
            _access.InsertAccount(new AccountModel(fullName, email, phoneNumber, BCrypt.Net.BCrypt.HashPassword(password), 4));
            return true;
        }
        else
        {
            return false;
        }
    }


}