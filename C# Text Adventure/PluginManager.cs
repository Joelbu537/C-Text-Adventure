using System.ComponentModel;

namespace TextAdventure;

using System.Reflection;
using TextAdventure.PluginContract;
public static class PluginManager
{
    public static List<IPlugin> Plugins { get; } = new();
    public static void LoadPlugins()
    {
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
}