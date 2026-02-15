namespace TextAdventure;
public static class CommandQueue
{
    public static IEnumerable<string> Commands => Queue;

    private static readonly Queue<string> Queue = new();
    private const int QUEUE_CAPACITY = 32;

    public static void AddCommandToQueue(string command)
    {
        if (Queue.Count >= QUEUE_CAPACITY)
        {
            Queue.Dequeue();
        }

        Queue.Enqueue(command);
    }
}