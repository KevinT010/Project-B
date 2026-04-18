
public static class TableMap
{
    public static void Display(List<TableModel> availableTables, int selectedIndex)
    {
        HashSet<int> available = availableTables.Select(t => t.TableNumber).ToHashSet();
        int selected = -1;
        if (selectedIndex >= 0 && selectedIndex < availableTables.Count)
        {
            selected = availableTables[selectedIndex].TableNumber;
        }

        Console.WriteLine("=============================================================================================");
        Console.WriteLine("|               |                                                                           |");
        Console.WriteLine("|       WC      |                           BackArea                                        |");
        Console.WriteLine("|               |                                                                           |");
        Console.WriteLine("========--===================================================================================");
        Console.WriteLine("|                                                          |     |                          |");
        Console.Write("|   ~~~~~~~~~~~~~~~      ========   ===============        |     |         KITCHEN          |");
        Console.WriteLine();
        Console.Write("|   * KIDS  PLAY  *      |  ");
        Print("T1", available, selected);
        Console.Write("  |   |     ");
        Print("T2", available, selected);
        Console.WriteLine("      |        |     |                          |");

        Console.WriteLine("|   *    AREA     *      |      |   |             |        |     |                          |");
        Console.WriteLine("|   * o O o O o O *      ========   ===============        |     ===========================|");
        Console.WriteLine("|   * O o O o O o *                                        |                                |");

        Console.Write("|   * o O o O o O *      ==========================        |             Dim Sum            \\");
        Console.WriteLine();

        Console.Write("|   *             *      |          ");
        Print("T3", available, selected);
        Console.WriteLine("            |        =================================\\");

        Console.WriteLine("|   ~~~~~~~~~~~~~~~      |                        |                                         |");
        Console.WriteLine("|                        ==========================                                         |");
        Console.WriteLine("|                                                                                           |");

        Console.Write("|   =============================      ========     ========     ========     ========      \\");
        Console.WriteLine();

        Console.Write("|   |            ");
        Print("T14", available, selected);
        Console.Write("            |      |  ");
        Print("T5", available, selected);
        Console.Write("  |     |  ");
        Print("T6", available, selected);
        Console.Write("  |     |  ");
        Print("T7", available, selected);
        Console.Write("  |     |  ");
        Print("T8", available, selected);
        Console.WriteLine("  |      \\");

        Console.WriteLine("|   =============================      ========     ========     ========     ========      |");
        Console.WriteLine("|                                                                                           |");
        Console.WriteLine("|                                                                                           |");

        Console.Write("\\   ===============      ===============      ===============           ===============     \\");
        Console.WriteLine();

        Console.Write("|   |     ");
        Print("T10", available, selected);
        Console.Write("     |      |     ");
        Print("T11", available, selected);
        Console.Write("     |      |     ");
        Print("T12", available, selected);
        Console.Write("     |           |     ");
        Print("T13", available, selected);
        Console.WriteLine("     |");

        Console.WriteLine("|   ===============      ===============      ===============           ===============     |");
        Console.WriteLine("|                                                                                           |");
        Console.WriteLine("========================================Entrance=============================================");
    }


    public static void DisplayStatic()
    {
        Console.WriteLine("=============================================================================================");
        Console.WriteLine("|               |                                                                           |");
        Console.WriteLine("|       WC      |                           BackArea                                        |");
        Console.WriteLine("|               |                                                                           |");
        Console.WriteLine("========--===================================================================================");
        Console.WriteLine("|                                                          |     |                          |");
        Console.WriteLine("|   ~~~~~~~~~~~~~~~      ========   ===============        |     |         KITCHEN          |");
        Console.WriteLine("|   * KIDS  PLAY  *      |  T1  |   |     T2      |        |     |                          |");
        Console.WriteLine("|   *    AREA     *      |      |   |             |        |     |                          |");
        Console.WriteLine("|   * o O o O o O *      ========   ===============        |     ===========================|");
        Console.WriteLine("|   * O o O o O o *                                        |                                |");
        Console.WriteLine("|   * o O o O o O *      ==========================        |             Dim Sum            \\");
        Console.WriteLine("|   *             *      |          T3            |        =================================\\");
        Console.WriteLine("|   ~~~~~~~~~~~~~~~      |                        |                                         |");
        Console.WriteLine("|                        ==========================                                         |");
        Console.WriteLine("|                                                                                           |");
        Console.WriteLine("|   =============================      ========     ========     ========     ========      \\");
        Console.WriteLine("|   |            T14            |      |  T5  |     |  T6  |     |  T7  |     |  T8  |      \\");
        Console.WriteLine("|   |                           |      |      |     |      |     |      |     |      |      |");
        Console.WriteLine("|   =============================      ========     ========     ========     ========      |");
        Console.WriteLine("|                                                                                           |");
        Console.WriteLine("|                                                                                           |");
        Console.WriteLine("\\   ===============      ===============      ===============           ===============     \\");
        Console.WriteLine("\\   |     T10     |      |     T11     |      |     T12     |           |     T13     |     \\");
        Console.WriteLine("|   |             |      |             |      |             |           |             |     |");
        Console.WriteLine("|   ===============      ===============      ===============           ===============     |");
        Console.WriteLine("|                                                                                           |");
        Console.WriteLine("========================================Entrance=============================================");
    }

    private static void Print(string table, HashSet<int> available, int selected)
    {
        int num = int.Parse(table.Substring(1));

        if (num == selected)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
        }
        else if (available.Contains(num))
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
        }

        Console.Write(table);
        Console.ResetColor();
    }
}


// What is the goal:

// We need to dynamicly display a map in asscii with dinning tables from the database
// And a user has to be able to choose a table based on this list of available tables 


// what do we have rn:
// -- List<TableModel> availableTables = reservationLogic.GetAvailableTables(requestedDateTime, numberOfGuests);
// which will provide me with a list of available tables based on the reservation time and number of guests


// -- List<string> tableOptions = new List<string>();
// -- Ui tableMenu = new Ui("Select a table", tableOptions.ToArray());  

