public class Ui
{
    public int SelectedIndex;
    public string[] Options;
    public string Prompt;

    public Action<int>? OnAfterDraw;
    public Action<int>? OnBeforeDraw;

    public Ui(string prompt, string[] options)
    {
        Prompt = prompt;
        Options = options;
        SelectedIndex = 0;
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

        string dashes = "";

        for (int i = 0; i < width; i++)
        {
            dashes += "-";
        }

        string border = "+" + dashes + "+";

        Console.WriteLine(Prompt);
        Console.WriteLine(border);

        for (int i = 0; i < Options.Length; i++)
        {
            string currentOption = Options[i];
            string prefix = i == SelectedIndex ? "> " : "  ";
            string line = $"| {prefix}{currentOption}".PadRight(width + 1) + "|";

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
        Console.WriteLine(border);
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
}