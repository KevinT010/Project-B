public class OperatingHour
{
    private OperatingHourLogic _logic = new();

    public void Start()
    {
        Console.Clear();
        Console.WriteLine("Opening hours:");
        Console.WriteLine("─────────────────────────────");

        var hours = _logic.GetHours();

        foreach (var day in hours)
        {
            if (day.IsClosed)
            {
                Console.WriteLine($"{day.Day}: Closed");
            }
            else
            {
                Console.WriteLine($"{day.Day}: {day.OpeningTime} - {day.ClosingTime}");
            }
        }

        Console.WriteLine("─────────────────────────────");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
        ReturnToPreviousMenu();
        return;
    }

    public void ReturnToPreviousMenu()
    {
        if (Session.CurrentUser != null)
        {
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
            Thread.Sleep(2000);
        }
        else
        {
            StartMenu.Start();
        }
    }
}