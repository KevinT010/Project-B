public class AccountManagementLogic
{
    private AccountRegistrationAccess _access = new();
    private ReservationAccess _reservationAccess = new();
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
        if (!_registrationLogic.EmailValidation(email))
        {
            return null;
        }

        return Session.CurrentUser;
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

    public void DeleteAccount(long id)
    {
        _reservationAccess.DeleteReservationsByUser(id);
        _access.DeleteAccount((int)id);
    }
}