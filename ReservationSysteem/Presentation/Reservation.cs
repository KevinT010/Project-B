using System.Text;

public class Reservation
{
    public void Start(AccountModel account)
    {

        // null check
        if (account != null)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ReservationLogic reservationLogic = new ReservationLogic();

            // date parsing
            Console.Write("Enter reservation date (dd-MM-yyyy) or 'Cancel' to go back to the main menu: ");
            string date_input = Console.ReadLine();
            if (date_input.ToLower() == "cancel")
            {
                Console.WriteLine("Reservation canceled, returning to the main menu.");
                Thread.Sleep(2000);
                AccountVisibility.VisibilityMenu(Session.CurrentUser);
                return;
            }

            string[] dateParts = date_input.Split('-');

            // if parts does not equal 3 check
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

            Console.Write("Enter reservation time (HH:mm) or 'Cancel' to go back to the main menu: ");
            string time_input = Console.ReadLine();
            if (time_input.ToLower() == "cancel")
            {
                Console.WriteLine("Reservation canceled, returning to the main menu.");
                Thread.Sleep(2000);
                AccountVisibility.VisibilityMenu(Session.CurrentUser);
                return;
            }

            string[] timeParts = time_input.Split(':');

            // if parts does not equal 2 check
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

            // reservation date check
            if (requestedDateTime < DateTime.Now)
            {
                Console.WriteLine("You can't make a reservation in the past. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }
            else if (requestedDateTime > DateTime.Now.AddYears(1))
            {
                Console.WriteLine("You can't make reservations further than a year in the future. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }


            Console.Write("Enter number of guests: ");
            int numberOfGuests = Convert.ToInt32(Console.ReadLine());

            // number of guest check
            while (numberOfGuests < 1 || numberOfGuests > 8)
            {
                Console.WriteLine("Invalid number of guests. You can only select between 1 and 8 guests.\nPress any key to go back.");
                Console.ReadKey();
                Console.Write("Enter number of guests: "); 
                numberOfGuests = Convert.ToInt32(Console.ReadLine());
            }

            Console.Write("Enter number of kids: ");
            int numberOfKids = Convert.ToInt32(Console.ReadLine());

            while (0 > numberOfKids || numberOfKids > numberOfGuests)
            {
                Console.WriteLine($"Invalid number of kids. The amount must be between 0 and {numberOfGuests}.\nPress any key to go back.");
                Console.ReadKey();
                Console.Write("Enter number of kids: "); 
                numberOfKids = Convert.ToInt32(Console.ReadLine());
            }

            // ----------------------------------------------------------------------------------------------------------------------------------

            List<TableModel> availableTables = reservationLogic.GetAvailableTables(requestedDateTime, numberOfGuests);

            // null check 
            if (availableTables.Count == 0)
            {
                Console.WriteLine($"Sorry, there are no available tables at this time for a group of {numberOfGuests}. Press any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }


            List<string> tableOptions = new List<string>();

            // add & convert available table object to string list  
            foreach (TableModel table in availableTables)
            {
                tableOptions.Add($"Table {table.TableNumber} (seats {table.Capacity})");
            }
            tableOptions.Add("Cancel");

            Ui tableMenu = new Ui("Select a table", tableOptions.ToArray());
            tableMenu.OnBeforeDraw = (index) =>
            {
                TableMap.Display(availableTables, index);
            };
            int selectedIndex = tableMenu.Run();

            if (tableOptions[selectedIndex] == "Cancel")
            {
               Start(account);
               return;
            }

            TableModel selectedTable = availableTables[selectedIndex];

            bool success = reservationLogic.MakeReservation(account.Id, selectedTable.Id, requestedDateTime, numberOfGuests, numberOfKids);

            if (success)
            {
                RewardLogic rewardLogic = new RewardLogic();
                rewardLogic.GiveReservationPoints(Session.CurrentUser);
                Console.WriteLine($"\n✅ Reservation confirmed!");
                Console.WriteLine($"   Table:     {selectedTable.TableNumber}");
                Console.WriteLine($"   Date & Time: {requestedDateTime:dd-MM-yyyy HH:mm}");
                Console.WriteLine($"   Adults:    {numberOfGuests - numberOfKids}");
                Console.WriteLine($"   Kids:    {numberOfKids}");
                Console.WriteLine($"   Duration:  2 hours");
                Console.WriteLine($"   Reward points earned:  +20");
            }

            else
            {
                Console.WriteLine("❌ Reservation failed. The table was just taken. Please try again.");
            }

            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
        }

        else
        {
            Console.WriteLine("You must be logged in to make a reservation first. Press any key to go back.");
            Console.ReadKey();
            StartMenu.Start();
        }
    }
}