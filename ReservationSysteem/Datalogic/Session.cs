public static class Session
{
    public static AccountModel CurrentUser { get; set; }

    public static void Login(AccountModel account)
    {
        CurrentUser = account;
    }

    public static void Logout()
    {
        CurrentUser = null;
        StartMenu.Start();
    }

}