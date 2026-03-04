using System.Reflection;
using TextAdventure.PluginContract;
namespace TextAdventure;
public static class PluginManager
{
    public static List<IPlugin> Plugins { get; } = new();
    public static void LoadPlugins()
    {
        string pluginDir = Path.Combine(AppContext.BaseDirectory, "Plugins");
        Directory.CreateDirectory(pluginDir);
        foreach(string dll in Directory.EnumerateFiles(pluginDir, "*.dll"))
        {
            try
            {
                var asm = Assembly.LoadFrom(dll);

                foreach (var t in asm.GetTypes())
                {
                    if (t.IsAbstract || !typeof(IPlugin).IsAssignableFrom(t)) continue;

                    if (Activator.CreateInstance(t) is IPlugin plugin)
                    {
                        if (Plugins.Any(other => other.Name.ToLower() == plugin.Name.ToLower()))
                        {
                            Console.WriteLine($"{Color.FORE_RED}Failed to load{Color.RESET} plugin {plugin.Name} ({Path.GetFileName(dll)}) because another plugin with the same name is already loaded.");
                            continue;
                        }
                        Plugins.Add(plugin);
                        Console.WriteLine($"Loaded plugin {Color.FORE_WHITE}{plugin.Name}{Color.RESET} ({Path.GetFileName(dll)}) {plugin.Version}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Color.FORE_RED}Failed to load{Color.RESET} plugin {Path.GetFileName(dll)}\n{ex.Message}\n{ex.StackTrace}");
            }
        }
    }
    public static void PluginManagerDisplay()
    {
        int selectedIndex = 0;
        while (true)
        {
            // Draw
            for(int i = 0; i < Plugins.Count; i++)
            {
                if(i == selectedIndex)
                {
                    Console.Write(Color.BACK_WHITE + Color.FORE_BLACK);
                }

                Console.Write(Color.RESET);
            }
            // Read
            while (true)
            {
                if(Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Escape) return;
                }
            }
            // Clear
            Console.Clear();
        }
    }
}