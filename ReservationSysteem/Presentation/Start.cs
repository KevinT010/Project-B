class RestaurantSystem
{
    static void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            ShowMenu();

            Console.Write("Select an option: ");
            string input = Console.ReadLine() ?? string.Empty;
            Console.Clear();

            switch (input)
            {
                case "1":
                    Console.WriteLine("You selected: View Menu");
                    break;

                case "2":
                    Console.WriteLine("You selected: Place Order");
                    break;

                case "3":
                    Console.WriteLine("You selected: View Reservations");
                    break;

                case "0":
                    Console.WriteLine("Exiting the system...");
                    isRunning = false;
                    continue;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to return to the start menu...");
            Console.ReadKey();
        }
    }

    static void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Restaurant System ===");
        Console.WriteLine("1. View Menu");
        Console.WriteLine("2. Place Order");
        Console.WriteLine("3. View Reservations");
        Console.WriteLine("0. Exit");
        Console.WriteLine();
    }
}