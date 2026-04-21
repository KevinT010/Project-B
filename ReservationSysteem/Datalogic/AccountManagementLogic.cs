public class AccountManagementLogic
{
    private AccountRegistrationAccess _access = new();
    private ReservationAccess _reservationAccess = new();

    public AccountManagementLogic()
    {
    }

    public void UpdateAccount(AccountModel account)
    {
        if (!string.IsNullOrWhiteSpace(account.Password))
            account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);

        _access.UpdateAccount(account);
    }

    public void DeleteAccount(long id)
    {
        _reservationAccess.DeleteReservationsByUser(id);
        _access.DeleteAccount((int)id);
    }
}