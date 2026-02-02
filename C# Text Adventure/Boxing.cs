namespace C__Text_Adventure;
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
        return content.Insert(0, "║ ") + C__Text_Adventure.Color.RESET + " ║";
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
        foreach(string color in C__Text_Adventure.Color.COLOR_LIST)
        {
            if(content.Contains(color)) content = content.Replace(color, "");
        }
        return content;
    }
    public static void WriteCentered(string content)
    {
        int consoleWidth = Console.WindowWidth;
        int contentLength = content.Clean().Length;
        int leftPadding = (consoleWidth - contentLength) / 2;
        Console.WriteLine(new string(' ', leftPadding) + content);
    }
    public static void WriteLineCentered(string content)
    {
        int consoleWidth = Console.WindowWidth;
        int contentLength = content.Clean().Length;
        int leftPadding = (consoleWidth - contentLength) / 2;
        Console.WriteLine(new string(' ', leftPadding) + content);
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
}