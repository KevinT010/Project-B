public class Ui
{
    public int SelectedIndex;
    public string[] Options;
    public string Prompt;
    public bool[] ToggledOptions;

    public Action<int>? OnAfterDraw;
    public Action<int>? OnBeforeDraw;

    public Ui(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
        SelectedIndex = 0;
        ToggledOptions = new bool[options.Length];
    }

    public void DisplayOptions()
    {
        int longest = 0;

        for (int i = 0; i < Options.Length; i++)
        {
            if (Options[i].Length > longest)
            {
                longest = Options[i].Length;
            }
        }

        int width = longest + 8;

        string horizontal = new string('─', width);
        string topBorder = "╔" + horizontal + "╗";
        string bottomBorder = "╚" + horizontal + "╝";

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ╻  ╻┏┓╻┏━╸   ╻  ╻┏┓╻┏━╸   ╻ ╻╻ ╻");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("★ ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("┃  ┃┃┗┫┃╺┓   ┃  ┃┃┗┫┃╺┓   ┃╻┃┃ ┃");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" ★");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ┗━╸╹╹ ╹┗━┛   ┗━╸╹╹ ╹┗━┛   ┗┻┛┗━┛");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Prompt);
        Console.ResetColor();
        Console.WriteLine(topBorder);

        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            string prefix = i == SelectedIndex ? "> " : "  ";
            string line = $"│ {prefix}{currentOption}".PadRight(width + 1) + "│";
            
            if (i == SelectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(line);
            Console.ResetColor();
        }
        Console.WriteLine(bottomBorder);
    }

    public void DisplayMultiOptions()
    {
        int longest = 0;

        for (int i = 0; i < Options.Length; i++)
        {
            if (Options[i].Length + 4 > longest)
            {
                longest = Options[i].Length + 4;
            }
        }

        int width = longest + 8;

        string horizontal = new string('─', width);
        string topBorder = "╔" + horizontal + "╗";
        string bottomBorder = "╚" + horizontal + "╝";

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ╻  ╻┏┓╻┏━╸   ╻  ╻┏┓╻┏━╸   ╻ ╻╻ ╻");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("★ ");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("┃  ┃┃┗┫┃╺┓   ┃  ┃┃┗┫┃╺┓   ┃╻┃┃ ┃");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(" ★");
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("  ┗━╸╹╹ ╹┗━┛   ┗━╸╹╹ ╹┗━┛   ┗┻┛┗━┛");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(Prompt);
        Console.ResetColor();
        Console.WriteLine(topBorder);

        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            string prefix = i == SelectedIndex ? "> " : "  ";
            string checkBox = "";

            if (currentOption != "Done")
            {
                checkBox = ToggledOptions[i] ? "[X] " : "[ ] ";
            }

            string line = $"│ {prefix}{checkBox}{currentOption}".PadRight(width + 1) + "│";
            
            if (i == SelectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(line);
            Console.ResetColor();
        }
        Console.WriteLine(bottomBorder);
    }

    public int Run()
    {
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            OnBeforeDraw?.Invoke(SelectedIndex);
            DisplayOptions();
            OnAfterDraw?.Invoke(SelectedIndex);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                SelectedIndex--;

                if (SelectedIndex < 0)
                {
                    SelectedIndex = Options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                SelectedIndex++;

                if (SelectedIndex >= Options.Length)
                {
                    SelectedIndex = 0;
                }
            }

        } while (keyPressed != ConsoleKey.Enter);

        return SelectedIndex;
    }

    public List<string> MultiSelect()
    {
        ConsoleKey keyPressed;
        do
        {
            Console.Clear();
            OnBeforeDraw?.Invoke(SelectedIndex);
            DisplayMultiOptions();
            OnAfterDraw?.Invoke(SelectedIndex);

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            keyPressed = keyInfo.Key;

            if (keyPressed == ConsoleKey.UpArrow)
            {
                SelectedIndex--;

                if (SelectedIndex < 0)
                {
                    SelectedIndex = Options.Length - 1;
                }
            }
            else if (keyPressed == ConsoleKey.DownArrow)
            {
                SelectedIndex++;

                if (SelectedIndex >= Options.Length)
                {
                    SelectedIndex = 0;
                }
            }
            else if (keyPressed == ConsoleKey.Spacebar || keyPressed == ConsoleKey.Enter)
            {
                if (Options[SelectedIndex] == "Done")
                {
                    break;
                }
                ToggledOptions[SelectedIndex] = !ToggledOptions[SelectedIndex];
            }

        } while (true);

        List<string> selected = new List<string>();
        for (int i = 0; i < Options.Length; i++)
        {
            if (ToggledOptions[i] && Options[i] != "Done")
            {
                selected.Add(Options[i]);
            }
        }
        return selected;
    }
}