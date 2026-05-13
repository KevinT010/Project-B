using System.Reflection;

public class PointShop
{
    private RewardLogic rewardLogic = new();
    public void Start(AccountModel account)
    {
        Console.Clear();
        string shopPrompt = $"What would you like to do?\nYou currently have: {account.DesertVouchers} vouchers, and {account.Points} points.";
        string[] shop_options = { "Buy Vouchers", "Return Vouchers", "Go back" };
        Ui pointsMenu = new Ui(shopPrompt, shop_options);
        int selectedIndex = pointsMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                RedeemDessertVoucher(account);
                break;
            case 1:
                if (account.DesertVouchers != 0)
                    {
                        ReturnDesertVoucher(account);
                    }
                else
                    {
                        Console.WriteLine("You currently don't have any vouchers");
                        Thread.Sleep(2000);
                        Start(account);
                    }
                break;
            case 2:
                AccountVisibility.VisibilityMenu(account);
                break;
        }
    }
    
    private void RedeemDessertVoucher(AccountModel account)
    {
        string buyPrompt = $"Which voucher do you wish to buy?\nYou currently have: {account.DesertVouchers} desert vouchers, and {account.Points} points.";
        string[] buy_options = { "Free dessert (200 points)", "Go back" };
        Ui buyMenu = new Ui(buyPrompt, buy_options);
        int selectedIndex = buyMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                if (account.Points >= 200)
                {
                    Console.WriteLine("How many vouchers would you like to buy?");
                    int Voucher_amount = Convert.ToInt32(Console.ReadLine());
                    BuyVouchers(account, Voucher_amount);
                }
                else
                {
                    Console.WriteLine("You unfortunately don't have enough points to buy vouchers");
                    Thread.Sleep(2000);
                    RedeemDessertVoucher(account);
                }
                break;
            case 1:
                AccountVisibility.VisibilityMenu(account);
                break;
        }
    }

    private void BuyVouchers(AccountModel account, int amount)
    {
        Console.Clear();
        bool success = rewardLogic.Add_Vouchers(account, amount);

        if (!success)
        {
            Console.WriteLine($"You don't have enough points. You need {amount * 200 - account.Points} more points.");
            Console.ReadKey();
            RedeemDessertVoucher(account);
            return;
        }

        Console.WriteLine($"✅ {amount} Voucher(s) claimed!");
        Console.WriteLine($"You currently have: {account.Points} points.");
        Console.ReadKey();
        AccountVisibility.VisibilityMenu(account);
    }

    private void ReturnDesertVoucher(AccountModel account)
    {
        string returnPrompt = $"How many vouchers do you wish to return?\nYou currently have: {account.DesertVouchers} desert vouchers, and {account.Points} points.";
        string[] return_options = { $"Free dessert Voucher (200 points) x {account.DesertVouchers}", "Go back" };
        Ui returnMenu = new Ui(returnPrompt, return_options);
        int selectedIndex = returnMenu.Run();

        switch (selectedIndex)
        {
            case 0:
                Console.WriteLine("How many vouchers would you like to sell?");
                int Voucher_amount = Convert.ToInt32(Console.ReadLine());
                SellVouchers(account, Voucher_amount);
                break;
            case 1:
                AccountVisibility.VisibilityMenu(account);
                break;
        }

        
    }

    private void SellVouchers(AccountModel account, int amount)
    {
        Console.Clear();
        bool success = rewardLogic.Remove_Vouchers(account, amount);

        if (!success)
        {
            Console.WriteLine($"Transaction failed\nYou only have {account.DesertVouchers} vouchers.");
            Console.ReadKey();
            ReturnDesertVoucher(account);
            return;
        }

        Console.WriteLine($"✅ {amount} Voucher(s) returned, {amount * 200} points added!");
        Console.WriteLine($"You currently have: {account.DesertVouchers} desert vouchers, and {account.Points} points.");
        Console.ReadKey();
        AccountVisibility.VisibilityMenu(account);
    }



}
