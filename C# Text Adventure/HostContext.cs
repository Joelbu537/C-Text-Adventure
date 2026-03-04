using System.Linq.Expressions;
using TextAdventure.PluginContract;

namespace TextAdventure;
public sealed class HostContext : IHostContext
{
    public void RegisterCommand(string commandName, Action<string[]> action)
    {
        if (Program.Commands.ContainsKey(commandName)) return;

        Program.Commands[commandName] = action;
    }
    public void OverwriteCommand(string commandName, Action<string[]> action)
    {
        Program.Commands[commandName] = action;
    }
}