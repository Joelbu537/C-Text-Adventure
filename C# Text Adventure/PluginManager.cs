namespace TextAdventure;

using System.Reflection;
using TextAdventure.PluginContract;
public static class PluginManager
{
    public static List<IPlugin> Plugins { get; private set; } = new();
    public static void LoadPlugins()
    {
        List<IPlugin> plugins = new();

        foreach(string dll in Directory.EnumerateFiles(Path.Combine(AppContext.BaseDirectory, "Plugins"), "*.dll"))
        {
            try
            {
                var asm = Assembly.LoadFrom(dll);

                foreach (var t in asm.GetTypes())
                {
                    if (t.IsAbstract) continue;
                    if (!typeof(IPlugin).IsAssignableFrom(t)) continue;

                    if (Activator.CreateInstance(t) is IPlugin plugin)
                    {
                        plugins.Add(plugin);
                        Console.WriteLine($"Loaded plugin {plugin.Name} {plugin.Version}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Color.FORE_RED}Failed to load{Color.RESET} plugin {dll}\n{ex.Message}");
            }
        }

        return plugins;
    }
}