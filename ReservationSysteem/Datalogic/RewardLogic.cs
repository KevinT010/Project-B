public class RewardLogic
{
    private AccountRegistrationAccess _access = new();

    public void GivePoints(AccountModel account, double priceTotal)
    {
        int earnedPoints = (int)priceTotal;
        account.Points += earnedPoints;
        _access.UpdatePoints(account.Id, account.Points);
    }

    public void SpendPoints(AccountModel account, int points)
    {
        account.Points -= points;
        _access.UpdatePoints(account.Id, account.Points);
    }

    public void GiveReservationPoints(AccountModel account)
    {
        account.Points += 20;
        _access.UpdatePoints(account.Id, account.Points);
    }
}