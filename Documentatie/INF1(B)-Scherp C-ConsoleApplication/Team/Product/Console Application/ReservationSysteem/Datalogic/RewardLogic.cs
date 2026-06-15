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

    public bool Add_Vouchers(AccountModel account, int amount)
    {
        int cost = amount * 200;
        if (account.Points < cost)
            return false;

        account.DesertVouchers += amount;
        account.Points -= cost;

        _access.UpdatePoints(account.Id, account.Points);
        _access.UpdateVouchers(account.Id, account.DesertVouchers);
        return true;
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

    public bool Remove_Vouchers(AccountModel account, int amount)
    {
        if (account.DesertVouchers < amount)
            return false;

        account.DesertVouchers -= amount;
        account.Points += amount * 200;

        if (account.Points > 20000)
            account.Points = 20000;

        _access.UpdatePoints(account.Id, account.Points);
        _access.UpdateVouchers(account.Id, account.DesertVouchers);
        return true;
    }

    public bool HasReachedMaxPoints(AccountModel account)
    {
        int maxPoints = 20000;
        return account.Points >= maxPoints;
    }

    public bool enoughPoints(AccountModel account, int amount)
    {
        int cost = amount * 200;
        return account.Points >= cost;
    }
}