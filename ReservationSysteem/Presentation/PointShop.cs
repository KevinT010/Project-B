using System.Reflection;

public class PointShop
{
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
                    ReturnDesertVoucher(account);
                else
                    Console.WriteLine("You currently don't have any vouchers");
                    Thread.Sleep(2000);
                    Start(account);
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
                Console.WriteLine("How many vouchers would you like to buy?");
                int Voucher_amount = Convert.ToInt32(Console.ReadLine());
                BuyVouchers(account, Voucher_amount);
                break;
            case 1:
                AccountVisibility.VisibilityMenu(account);
                break;
        }
    }

    private void BuyVouchers(AccountModel account, int amount)
    {
        Console.Clear();
        if (account.Points < amount * 200)
        {
            Console.WriteLine($"You unfortunately don't have enough points. You need {amount * 200 - account.Points} more points to claim {amount} voucher(s).");
            Console.WriteLine($"Press any key to return to return");
            Console.ReadKey();
            RedeemDessertVoucher(account);
            return;
        }

        RewardLogic rewardLogic = new RewardLogic();
        rewardLogic.Add_Vouchers(account, amount);
        Console.WriteLine($"✅ {amount} Voucher(s) claimed, enjoy!");
        Console.WriteLine($"You currently have: {account.Points} points.");
        Console.WriteLine("\nPress any key to return...");
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
        if (account.DesertVouchers < amount)
        {
            Console.WriteLine($"You unfortunately don't have that many vouchers. You currently have {account.DesertVouchers} vouchers.");
            Console.WriteLine($"Press any key to return.");
            Console.ReadKey();
            ReturnDesertVoucher(account);
            return;
        }

        if (account.Points + amount * 200 > 20000)
        {
            string confirmationPrompt = $"You currently have {account.Points}, returning {amount} of vouchers will put you over the limit of 20000 and thus set your point amount to 20000.";
            string[] confirmation_options = { $"Yes", "No" };
            Ui confirmationMenu = new Ui(confirmationPrompt, confirmation_options);
            int selectedIndex = confirmationMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("Transaction canceled");
                    ReturnDesertVoucher(account);
                    break;
            }
        }

        RewardLogic rewardLogic = new RewardLogic();
        rewardLogic.Remove_Vouchers(account, amount);
        Console.WriteLine($"✅ Vouchers returned, your {amount * 200} points have been returned to your account!");
        Console.WriteLine($"You currently have: {account.Points} points.");
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
        AccountVisibility.VisibilityMenu(account);
    }



}
