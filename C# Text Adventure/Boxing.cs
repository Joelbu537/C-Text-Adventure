namespace TextAdventure;
public static class Boxing
{
    public static string WindowCeiling(int width)
    {
        return $"╔{new string('═', width + 2)}╗";
    }
    public static string WindowFloor(int width)
    {
        return $"╚{new string('═', width + 2)}╝";
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
        if (startIndex < 0 || startIndex > original.Clean().Length) throw new ArgumentOutOfRangeException();
        int endIndex = Math.Min(startIndex + input.Clean().Length, original.Clean().Length);

        return original.Substring(0, startIndex) + input + original.Substring(endIndex);
    }
    public static string[] WrapText(string text, int maxCharsPerLine) // !!THIS METHOD IS AI-GENERATED!!
    {
        if (string.IsNullOrWhiteSpace(text))
            return Array.Empty<string>();
    
        List<string> lines = new();
    
        // 1️⃣ Harte Zeilenumbrüche beachten
        string[] rawLines = text.Split('\n');
    
        foreach (string rawLine in rawLines)
        {
            // Leere Zeile erzwingen
            if (string.IsNullOrWhiteSpace(rawLine))
            {
                lines.Add(string.Empty);
                continue;
            }
    
            string[] words = rawLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string currentLine = "";
    
            foreach (string word in words)
            {
                // Wort ist länger als eine ganze Zeile → hart splitten
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
    
}