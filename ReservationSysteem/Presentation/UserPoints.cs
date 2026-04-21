public class UserPoints
{
    public void Start(AccountModel account)
    {
        Console.Clear();
        Console.WriteLine($"You currently have: {account.Points} points.");
        Console.WriteLine("\nPress any key to return...");
        Console.ReadKey();
        AccountVisibility.VisibilityMenu(account);
    }
}