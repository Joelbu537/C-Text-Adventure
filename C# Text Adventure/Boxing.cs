namespace TextAdventure;
public static class Boxing
{
    public static string WindowCeiling(int width)
    {
        return $"╔{new string('═', width + 2)}╗";
    }
    public static string WindowCeiling(int width, string color)
    {
        return color + WindowCeiling(width) + Color.RESET;
    }
    public static string WindowFloor(int width)
    {
        return $"╚{new string('═', width + 2)}╝";
    }
    public static string WindowFloor(int width, string color)
    {
        return color + WindowFloor(width) + Color.RESET;
    }
    public static string WindowWall(string content)
    {
        return content.Insert(0, "║ ") + Color.RESET + " ║";
    }
    public static string WindowWall(string content, int targetWidth)
    {
        int cleanLength = content.Clean().Length;
        if (cleanLength < targetWidth)
        {
            return WindowWall(content + new string(' ', targetWidth - cleanLength));
        }
        else if (cleanLength > targetWidth)
        {
            return WindowWall(content.Substring(0, targetWidth));
        }
        return WindowWall(content);
    }
    public static string WindowWall(string content, string color)
    {
        return content.Insert(0, $"{color}║{TextAdventure.Color.RESET} ") + $" {color}║{TextAdventure.Color.RESET}";
    }
    public static string WindowWall(string content, int targetWidth, string color)
    {
        int cleanLength = content.Clean().Length;
        if (cleanLength < targetWidth)
        {
            return WindowWall(content + new string(' ', targetWidth - cleanLength), color);
        }
        else if (cleanLength > targetWidth)
        {
            return WindowWall(content.Substring(0, targetWidth), color);
        }
        return WindowWall(content, color);
    }
    public static string Clean(this string content)
    {
        foreach(string color in Color.COLOR_LIST)
        {
            if(content.Contains(color)) content = content.Replace(color, "");
        }
        return content;
    }
    public static string Center(string content)
    {
        int consoleWidth = Console.WindowWidth;
        int contentLength = content.Clean().Length;
        int leftPadding = (consoleWidth - contentLength) / 2;
        return new string(' ', leftPadding) + content;
    }
    public static string Center(string content, int width) // Cheap overload, does its job.
    {
        int consoleWidth = width;
        int contentLength = content.Clean().Length;
        int leftPadding = (consoleWidth - contentLength) / 2;
        return new string(' ', leftPadding) + content;
    }
    public static void WriteCentered(string content)
    {
        Console.WriteLine(Center(content));
    }
    public static void WriteLineCentered(string content)
    {
        Console.WriteLine(Center(content));
    }
    public static void WindowTooSmall()
    {
        string warning = Color.FORE_LIGHT_RED + "WINDOW TOO SMALL!";
        Console.Clear();
        WriteLineCentered(WindowCeiling(warning.Clean().Length));
        WriteLineCentered(WindowWall(warning));
        WriteLineCentered(WindowWall($"{Console.WindowWidth}x{Console.WindowHeight} detected!", warning.Clean().Length));
        WriteLineCentered(WindowFloor(warning.Clean().Length));
    }
    public static string OverwriteAt(this string original, string input, int startIndex) // !!THIS METHOD IS AI-GENERATED but highly modified by myself!!
    {
        int originalCleanLength = original.Clean().Length;
        int inputCleanLength = input.Clean().Length;

        if (startIndex < 0 || startIndex > originalCleanLength) throw new ArgumentOutOfRangeException(nameof(startIndex));

        int endCleanIndex = Math.Min(
            startIndex + inputCleanLength,
            originalCleanLength
        );

        int rawStart = original.CleanIndexToRawIndex(startIndex);
        int rawEnd = original.CleanIndexToRawIndex(endCleanIndex);

        return original[..rawStart] + input + original[rawEnd..];
    }
    public static string[] WrapText(string text, int maxCharsPerLine) // !!THIS METHOD IS AI-GENERATED because I am lazy!!
    {
        if (string.IsNullOrWhiteSpace(text))
            return Array.Empty<string>();

        List<string> lines = new();

        string[] rawLines = text.Split('\n');

        foreach (string rawLine in rawLines)
        {

            if (string.IsNullOrWhiteSpace(rawLine))
            {
                lines.Add(string.Empty);
                continue;
            }

            string[] words = rawLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string currentLine = "";

            foreach (string word in words)
            {
                if (word.Clean().Length > maxCharsPerLine)
                {
                    if (currentLine.Clean().Length > 0)
                    {
                        lines.Add(currentLine);
                        currentLine = "";
                    }

                    for (int i = 0; i < word.Clean().Length; i += maxCharsPerLine)
                    {
                        int len = Math.Min(maxCharsPerLine, word.Clean().Length - i);
                        lines.Add(word.Substring(i, len));
                    }
                    continue;
                }

                if (currentLine.Clean().Length == 0)
                {
                    currentLine = word;
                }
                else if (currentLine.Clean().Length + 1 + word.Clean().Length <= maxCharsPerLine)
                {
                    currentLine += " " + word;
                }
                else
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
            }

            if (currentLine.Clean().Length > 0)
                lines.Add(currentLine);
        }

        return lines.ToArray();
    }
    public static int CleanIndexToRawIndex(this string s, int cleanIndex)
    {
        int raw = 0;
        int clean = 0;

        while (raw < s.Length && clean < cleanIndex)
        {
            if (s[raw] == '\x1b') // Find ANSI
            {
                while (raw < s.Length && s[raw] != 'm') // And skip it
                    raw++;

                raw++;
                continue;
            }
            raw++;
            clean++;
        }

        return raw;
    }
}