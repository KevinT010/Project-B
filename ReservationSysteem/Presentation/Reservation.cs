using System.Text;

public class Reservation
{
    public void Start(AccountModel account)
    {
        if (account != null)
        {
            Console.OutputEncoding = Encoding.UTF8;
            ReservationLogic reservationLogic = new();
            RewardLogic rewardLogic = new RewardLogic();
            List<ReservationModel> b = reservationLogic.GetActiveByAccountId(account.Id);
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

            if (dateParts.Length != 3)
            {
                Console.WriteLine("Invalid date.\nPress any key to go back.");
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

            if (timeParts.Length != 2)
            {
                Console.WriteLine("Invalid time.\nPress any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }

            int hour = Convert.ToInt32(timeParts[0]);
            int minute = Convert.ToInt32(timeParts[1]);
            DateTime requestedDateTime = new DateTime(year, month, day, hour, minute, 0);

            if (requestedDateTime < DateTime.Now)
            {
                Console.WriteLine("You can't make a reservation in the past.\nPress any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }
            else if (requestedDateTime > DateTime.Now.AddYears(1))
            {
                Console.WriteLine("You can't make reservations further than a year in the future.\nPress any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }

            OperatingHourLogic operatingHourLogic = new OperatingHourLogic();
            var dayInformation = operatingHourLogic.GetByDay(requestedDateTime.ToString());

            if (!operatingHourLogic.IsOpen(requestedDateTime))
            {
                if (dayInformation == null || dayInformation.IsClosed)
                {
                    Console.WriteLine($"The restaurant is closed on {requestedDateTime.DayOfWeek}.");
                    var hours = operatingHourLogic.GetHours();
                    foreach (var operatingDay in hours)
                    {
                        if (operatingDay.IsClosed)
                        {
                            Console.WriteLine($"{operatingDay.Day}: Closed");
                        }
                        else
                        {
                            Console.WriteLine($"{operatingDay.Day}: {operatingDay.OpeningTime} - {operatingDay.ClosingTime}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"The restaurant is closed at this time. Operating hours for {requestedDateTime.DayOfWeek} are from {dayInformation.OpeningTime} to {dayInformation.ClosingTime}.");
                    var hours = operatingHourLogic.GetHours();
                    foreach (var operatingDay in hours)
                    {
                        if (operatingDay.IsClosed)
                        {
                            Console.WriteLine($"{operatingDay.Day}: Closed");
                        }
                        else
                        {
                            Console.WriteLine($"{operatingDay.Day}: {operatingDay.OpeningTime} - {operatingDay.ClosingTime}");
                        }
                    }
                }

                Start(account);
                return;
            }

            if (!operatingHourLogic.IsOpen(requestedDateTime.AddMinutes(120)))
            {
                if (dayInformation != null && !dayInformation.IsClosed)
                {
                    Console.WriteLine($"Reservations last 2 hours. The restaurant closes at {dayInformation.ClosingTime} on {requestedDateTime.DayOfWeek}.\nPlease choose an earlier time.");
                    var hours = operatingHourLogic.GetHours();
                    foreach (var operatingDay in hours)
                    {
                        if (operatingDay.IsClosed)
                        {
                            Console.WriteLine($"{operatingDay.Day}: Closed");
                        }
                        else
                        {
                            Console.WriteLine($"{operatingDay.Day}: {operatingDay.OpeningTime} - {operatingDay.ClosingTime}");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please choose an earlier time..");
                    var hours = operatingHourLogic.GetHours();
                    foreach (var operatingDay in hours)
                    {
                        if (operatingDay.IsClosed)
                        {
                            Console.WriteLine($"{operatingDay.Day}: Closed");
                        }
                        else
                        {
                            Console.WriteLine($"{operatingDay.Day}: {operatingDay.OpeningTime} - {operatingDay.ClosingTime}");
                        }
                    }
                }
                Start(account);
                return;
            }


            Console.Write("Enter number of total guests or type 'back' to return to the date selection: ");
            int numberOfGuests = 0;

            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "back")
                {
                    Console.Clear();
                    Start(account);
                    break;
                }

                if (int.TryParse(input, out numberOfGuests) || numberOfGuests < 1 || numberOfGuests > 12)
                {
                    if (numberOfGuests > 12)
                    {
                        Console.WriteLine("For parties with more than 12 persons, please contact the restaurant at: 0682618970\nPress any key to go back.");
                    }
                    else if (numberOfGuests < 1)
                    {
                        Console.WriteLine("You can not make a reservation for less than 1 person\nPress any key to go back.");
                    }
                    else
                    {
                        break;
                    }

                    Console.ReadKey();
                    Console.Clear();
                    Console.Write("Enter number of total guests or type 'back' to return to the date selection: ");
                }

            }

            Console.Write($"Enter the number of kids in the party of {numberOfGuests}, or type 'back' to return to the date selection: ");

            int numberOfKids;

            while (true)
            {
                string input = Console.ReadLine();

                if (input.ToLower() == "back")
                {
                    Console.Clear();
                    Start(account);
                    return;
                }

                if (int.TryParse(input, out numberOfKids))
                {
                    if (numberOfKids >= 0 && numberOfKids <= numberOfGuests)
                    {
                        break;
                    }

                    Console.WriteLine($"Invalid number of kids. The amount must be between 0 and {numberOfGuests}.");
                }

                else
                {
                    Console.WriteLine("Please enter a valid number or type 'back' to return to the date selection");
                }

                Console.ReadKey();
                Console.Clear();
                Console.Write($"Enter the number of kids in the party of {numberOfGuests}, or type 'back' to return to the date selection: ");
            }

            int kidsPlayArea = 0;

            if (numberOfKids > 0)
            {
                int currentKidsInPlayArea = reservationLogic.GetKidsInPlayArea(requestedDateTime, 120);
                int availableSpots = reservationLogic.MaxPlayAreaCapacity - currentKidsInPlayArea;

                Console.WriteLine("\n--- Kids Play Area Availability ---");
                for (int i = 0; i < reservationLogic.MaxPlayAreaCapacity; i++)
                {
                    if (i < currentKidsInPlayArea)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("[Taken] ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("[Open]  ");
                    }

                    if (i == 4)
                    {
                        Console.WriteLine();
                    }
                }
                Console.ResetColor();
                Console.WriteLine($"\n\n({availableSpots} out of {reservationLogic.MaxPlayAreaCapacity} spots remaining)");
                Console.WriteLine("-----------------------------------\n");
            }

            bool playAreaPicked = false;
            while (numberOfKids > 0 && !playAreaPicked && reservationLogic.CheckPlayAreaCapacity(requestedDateTime, numberOfKids))
            {
                Console.Write("Do you want to book a spot in the kids play area? (yes/no): ");
                string playAreaInput = Console.ReadLine().ToLower();

                if (playAreaInput == "yes")
                {
                    if (reservationLogic.CheckPlayAreaCapacity(requestedDateTime, numberOfKids))
                    {
                        kidsPlayArea = numberOfKids;
                        break;
                    }
                    else
                    {
                        Console.WriteLine($"kids playarea can only contain {reservationLogic.MaxPlayAreaCapacity} kids at this time. \nPress any key to go back.");
                        Console.ReadKey();
                    }
                }
                else if (playAreaInput == "no")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
            List<TableModel> availableTables = reservationLogic.GetAvailableTables(requestedDateTime, numberOfGuests);

            if (availableTables.Count == 0)
            {
                Console.WriteLine($"Sorry, there are no available tables at this time for a group of {numberOfGuests}.\nPress any key to go back.");
                Console.ReadKey();
                Start(account);
                return;
            }



            List<string> tableOptions = new List<string>();
  
            foreach (TableModel table in availableTables)
            {
                if (reservationLogic.CheckHibachiName(table))
                {
                    tableOptions.Add($"Hibachi Bar (seats {table.Capacity})");
                    continue;
                }

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

            Console.Clear();
            Console.Write("Do you have any notes or special requests or allergens? (Leave empty if none): ");
            string notes = Console.ReadLine();

            bool success = reservationLogic.MakeReservation(account.Id, selectedTable.Id, requestedDateTime, numberOfGuests, numberOfKids, kidsPlayArea, notes);

            if (success)
            {
                rewardLogic.GiveReservationPoints(Session.CurrentUser);
                Console.WriteLine($"\n✅ Reservation confirmed!");
                Console.WriteLine($"   Table:     {selectedTable.TableNumber}");
                Console.WriteLine($"   Date & Time: {requestedDateTime:dd-MM-yyyy HH:mm} -> {requestedDateTime.AddMinutes(120):HH:mm}");
                Console.WriteLine($"   Adults:    {numberOfGuests - numberOfKids}");
                Console.WriteLine($"   Kids:      {numberOfKids}");
                Console.WriteLine($"   Duration:  2 hours");
                Console.WriteLine("   You can now pre order in reservation view");
                if (rewardLogic.HasReachedMaxPoints(Session.CurrentUser))
                    Console.WriteLine($"   Reward points: +0 (Maximum point amount reached)");
                else
                    Console.WriteLine($"   Reward points:  +20");
                Console.WriteLine($"   Kids in play area:  {kidsPlayArea}");
                if (!string.IsNullOrEmpty(notes))
                {
                    Console.WriteLine($"   Notes: {notes}");
                }
            }

            else
            {
                Console.WriteLine("❌ Reservation failed. The table was just taken. Please try again.");
            }

            if (rewardLogic.HasReachedMaxPoints(Session.CurrentUser))
                Console.WriteLine("\nYou have enough points to claim a voucher!");
            Console.WriteLine("\nPress any key to return.");
            Console.ReadKey();
            AccountVisibility.VisibilityMenu(Session.CurrentUser);
        }


        Console.WriteLine("You must be logged in to make a reservation first.\nPress any key to go back.");
        Console.ReadKey();
        StartMenu.Start();



    }
}