public class AccountLoginLogic
{
    private AccountRegistrationAccess _access = new();
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

    public AccountModel AccountLoginValidation(string email, string password)
    {
        var account = _access.GetByEmail(email);
        if (account != null && BCrypt.Net.BCrypt.Verify(password, account.Password))
        {
            return account;
        }
        else
        {
            return null;
        }
    }
}