public class PointShop
{
    public void Start(AccountModel account)
    {
        Console.Clear();
        string shopPrompt = $"You currently have: {account.Points} points.";
        string[] shop_options = { "Free dessert (200 points)", "Go back" };
        Ui pointsMenu = new Ui(shopPrompt, shop_options);
        int selectedIndex = pointsMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                RedeemDessertVoucher(account);
                break;
            case 1:
                AccountVisibility.VisibilityMenu(account);
                break;
        }
    }
    
    private void RedeemDessertVoucher(AccountModel account)
    {
        Console.Clear();
        if (account.Points < 200)
        {
            Console.WriteLine($"You unfortunately don't have enough points. You need {200 - account.Points} more points to claim this voucher.");
            Console.WriteLine($"Press any key to return to the shop");
            Console.ReadKey();
            Start(account);
            return;
        }

        RewardLogic rewardLogic = new RewardLogic();
        rewardLogic.SpendPoints(account, 200);
        Console.WriteLine("✅ Voucher claimed, enjoy your dessert!");
        Console.WriteLine($"You currently have: {account.Points} points.");
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
        AccountVisibility.VisibilityMenu(account);
    }
}
