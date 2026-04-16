using System.Text;

public class Reservation
{
    public void Start(AccountModel account)
    {
        if(account != null)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ReservationLogic reservationLogic = new ReservationLogic();

            Console.Write("Enter reservation date (dd-MM-yyyy): ");
            string[] dateParts = Console.ReadLine().Split('-');

            if (dateParts.Length != 3)
            {
                Console.WriteLine("Invalid date. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }

            int day = Convert.ToInt32(dateParts[0]);
            int month = Convert.ToInt32(dateParts[1]);
            int year = Convert.ToInt32(dateParts[2]);
            DateTime date = new DateTime(year, month, day);

            Console.Write("Enter reservation time (HH:mm): ");
            string[] timeParts = Console.ReadLine().Split(':');

            if (timeParts.Length != 2)
            {
                Console.WriteLine("Invalid time. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }

            int hour = Convert.ToInt32(timeParts[0]);
            int minute = Convert.ToInt32(timeParts[1]);
            DateTime requestedDateTime = new DateTime(year, month, day, hour, minute, 0);

            if (requestedDateTime < DateTime.Now)
            {
                Console.WriteLine("You can't make a reservation in the past. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }


            Console.Write("Enter number of guests: ");
            int numberOfGuests = Convert.ToInt32(Console.ReadLine());

            if (numberOfGuests < 1)
            {
                Console.WriteLine("Invalid number of guests. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }

            List<TableModel> availableTables = reservationLogic.GetAvailableTables(requestedDateTime, numberOfGuests);

            if (availableTables.Count == 0)
            {
                Console.WriteLine($"Sorry, there are no available tables at this time for a group of {numberOfGuests}. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }

            List<string> tableOptions = new List<string>();
            foreach (TableModel table in availableTables)
            {
                tableOptions.Add($"Table {table.TableNumber} (seats {table.Capacity})");
            }
            tableOptions.Add("Cancel");

            Ui tableMenu = new Ui("Select a table", tableOptions.ToArray());

            int selectedIndex = tableMenu.Run();

            if (tableOptions[selectedIndex] == "Cancel")
            {
                Start(account);
                return;
            }

            TableModel selectedTable = availableTables[selectedIndex];

            bool success = reservationLogic.MakeReservation(account.Id, selectedTable.Id, requestedDateTime, numberOfGuests);

            if (success)
            {
                Console.WriteLine($"\n✅ Reservation confirmed!");
                Console.WriteLine($"   Table:     {selectedTable.TableNumber}");
                Console.WriteLine($"   Date & Time: {requestedDateTime:dd-MM-yyyy HH:mm}");
                Console.WriteLine($"   Guests:    {numberOfGuests}");
                Console.WriteLine($"   Duration:  2 hours");
            }
            else
            {
                Console.WriteLine("❌ Reservation failed. The table was just taken. Please try again.");
            }

            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
            StartMenu.Start();
        }

        else
        {
            Console.WriteLine("You must be logged in to make a reservation first. Press any key to go back.");
            Console.ReadKey();
            StartMenu.Start();
        }
    }
}