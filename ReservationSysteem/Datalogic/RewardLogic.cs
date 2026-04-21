public class RewardLogic
{
    private AccountRegistrationAccess _access = new();

    public void GivePoints(AccountModel account, double priceTotal)
    {
        int earnedPoints = (int)priceTotal;
        account.Points += earnedPoints;
        _access.UpdatePoints(account.Id, account.Points);
    }
}