public class Nothing
{
    public void start()
    {
        Console.Clear();
        Console.WriteLine("Nothing is here yet.");
        Thread.Sleep(1000);
        AccountVisibility.VisibilityMenu(Session.CurrentUser);
    }
}