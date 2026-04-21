public class AccountManagementLogic
{
    private AccountRegistrationAccess _access = new();
    private ReservationAccess _reservationAccess = new();
    private AccountRegistrationLogic _registrationLogic = new();

    public AccountManagementLogic()
    {
    }

    public bool UpdateAccount(AccountModel account)
    {
        if (!_registrationLogic.FirstNameValidation(account.FirstName)){
            return false;
        }
        if (!_registrationLogic.LastNameValidation(account.LastName)){
            return false;
        }
        if (!_registrationLogic.PhoneNumberValidation(account.PhoneNumber)){
            return false;
        }
        if (!string.IsNullOrWhiteSpace(account.Password))
        {
            if (!_registrationLogic.PasswordValidation(account.Password)){
                return false;
            }
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
        }

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