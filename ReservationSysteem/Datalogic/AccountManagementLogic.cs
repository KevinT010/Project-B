public class AccountManagementLogic
{
    private AccountRegistrationAccess _access = new();
    private ReservationLogic _reservationlogic = new();
    private AccountRegistrationLogic _registrationLogic = new();

    public AccountManagementLogic()
    {
    }

    public bool VerifyPassword(AccountModel account, string currentPassword)
    {
        return BCrypt.Net.BCrypt.Verify(currentPassword, account.Password);
    }

    public bool UpdatePassword(AccountModel account, string newPassword)
    {
        if (_registrationLogic.PasswordValidation(newPassword))
        {
            account.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            return true;
        }

        return false;
    }

    public AccountModel FirstNameValidation(string firstName)
    {
        if (!_registrationLogic.FirstNameValidation(firstName))
        {
            return null;
        }

        return Session.CurrentUser;
    }

    public AccountModel LastNameValidation(string lastName)
    {
        if (!_registrationLogic.LastNameValidation(lastName))
        {
            return null;
        }

        return Session.CurrentUser;
    }

    public AccountModel EmailValidation(string email)
    {
        int atIndex = email.IndexOf("@");
        int dotIndex = email.LastIndexOf(".");

        if (atIndex > 0 && dotIndex > atIndex && _access.GetByEmail(email) == null)
        {
            return Session.CurrentUser;
        }

        return null;
    }

    public AccountModel PhoneNumberValidation(string phoneNumber)
    {
        if (!_registrationLogic.PhoneNumberValidation(phoneNumber))
        {
            return null;
        }

        return Session.CurrentUser;
    }

    public AccountModel PasswordValidation(string password)
    {
        if (!_registrationLogic.PasswordValidation(password))
        {
            return null;
        }

        return Session.CurrentUser;
    }

    public bool UpdateAccount(AccountModel account)
    {
        _access.UpdateAccount(account);
        return true;
    }

    public bool DeleteAccount(long id)
    {
        _reservationlogic.DeleteReservationsByUser(id);
        _access.DeleteAccount((int)id);

        if (Session.CurrentUser != null && Session.CurrentUser.Id == id)
        {
            Session.CurrentUser = null;
        }

        return true;
    }


    public AccountModel GetUserByEmail(string email)
    {
        if(_registrationLogic.EmailValidation(email) == null)
        {
            return null;
        }

        return _access.GetByEmail(email);
    }
}