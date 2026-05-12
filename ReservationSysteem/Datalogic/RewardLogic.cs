public class RewardLogic
{
    private AccountRegistrationAccess _access = new();

    public void GivePoints(AccountModel account, double priceTotal)
    {
        int earnedPoints = (int)priceTotal;
        account.Points += earnedPoints;
        if (account.Points > 20000)
        {
            account.Points = 20000;
        }
        _access.UpdatePoints(account.Id, account.Points);
    }

    public void Add_Vouchers(AccountModel account, int amount)
    {
        account.DesertVouchers += amount;
        account.Points -= amount * 200;
        _access.UpdatePoints(account.Id, account.Points);
    }

    public void GiveReservationPoints(AccountModel account)
    {
        account.Points += 20;
        if (account.Points > 20000)
        {
            account.Points = 20000;
        }
        _access.UpdatePoints(account.Id, account.Points);
    }
    
    public void Remove_Vouchers(AccountModel account, int amount)
    {
        account.DesertVouchers -= amount;
        account.Points += amount * 200;
        _access.UpdatePoints(account.Id, account.Points);
    }
}