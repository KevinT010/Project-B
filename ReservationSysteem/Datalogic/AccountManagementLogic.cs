public class AccountManagementLogic
{
    private AccountRegistrationAccess _access = new();
    private ReservationAccess _reservationAccess = new();
    private AccountRegistrationLogic _registrationLogic = new();

    public AccountManagementLogic()
    {
    }

    public AccountModel FirstNameValidation(string firstName)
    {
        if (!_registrationLogic.FirstNameValidation(firstName))
            return null;

        AccountModel account = _access.UpdateAccount(Session.CurrentUser);
        account.FirstName = firstName;
        return account;
    }
    public AccountModel LastNameValidation(string lastName)
    {
        if (!_registrationLogic.LastNameValidation(lastName))
            return null;

        AccountModel account = _access.UpdateAccount(Session.CurrentUser);
        account.LastName = lastName;
        return account;
    }
    public AccountModel EmailValidation(string email)
    {
        if (!_registrationLogic.EmailValidation(email))
            return null;

        AccountModel account = _access.UpdateAccount(Session.CurrentUser);
        account.Email = email;
        return account;
    }
    public AccountModel PhoneNumberValidation(string phoneNumber)
    {
        if (!_registrationLogic.PhoneNumberValidation(phoneNumber))
            return null;

        AccountModel account = _access.UpdateAccount(Session.CurrentUser);
        account.PhoneNumber = phoneNumber;
        return account;
    }
    public AccountModel PasswordValidation(string password)
    {
        if (!_registrationLogic.PasswordValidation(password))
            return null;

        AccountModel account = _access.UpdateAccount(Session.CurrentUser);
        account.Password = password;
        return account;
    }
    public bool UpdateAccount(AccountModel account)

    {
        _access.UpdateAccount(account);
        {
        }
        return true;
    }

    public void DeleteAccount(long id)
    {
        _reservationAccess.DeleteReservationsByUser(id);
        {
        }
        _access.DeleteAccount((int)id);
        {
        }
    }
}