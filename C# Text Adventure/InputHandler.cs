using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure;
public static class InputHandler
{
    private static readonly Queue<string> Queue = new();
    public static string ReadInput(string textColor = Color.FORE_WHITE)
    {
        string inputRaw = string.Empty;
        int commandIndex = Queue.Count;

        Console.Write(textColor);

        while (true)
        {
            if (!Console.KeyAvailable) continue;

            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Enter && inputRaw.Length > 0)
            {
                break;
            }
            else if (key.Key == ConsoleKey.Backspace && inputRaw.Length > 0)
            {
                inputRaw = inputRaw.Substring(0, inputRaw.Length - 1);
                Console.Write("\b \b");
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                foreach (char c in inputRaw)
                {
                    Console.Write("\b \b");
                }

                inputRaw = string.Empty;
            }
            else if (key.Key == ConsoleKey.UpArrow)
            {
                commandIndex = Math.Clamp(commandIndex - 1, 0, Queue.Count);
                ClearInput(inputRaw.Length);
                inputRaw = string.Empty;

                if (commandIndex == Queue.Count) continue;

                inputRaw = Queue.ElementAt(commandIndex);
                Console.Write(Queue.ElementAt(commandIndex));
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                commandIndex = Math.Clamp(commandIndex + 1, 0, Queue.Count);
                ClearInput(inputRaw.Length);
                inputRaw = string.Empty;

                if (commandIndex == Queue.Count) continue;

                inputRaw = Queue.ElementAt(commandIndex);
                Console.Write(Queue.ElementAt(commandIndex));
            }
            else if (char.IsAsciiLetterOrDigit(key.KeyChar) || key.KeyChar == ' ')
            {
                inputRaw += key.KeyChar;
                Console.Write(key.KeyChar);
            }
        }

        Queue.Enqueue(inputRaw);
        Console.Write(Color.RESET);

        return inputRaw;
    }
    private static void ClearInput(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.Write('\b');
        }
        for (int i = 0; i < count; i++)
        {
            Console.Write(' ');
        }
        for (int i = 0; i < count; i++)
        {
            Console.Write('\b');
        }
    }
}