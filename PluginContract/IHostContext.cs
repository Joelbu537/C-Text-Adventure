namespace TextAdventure.PluginContract;
public interface IHostContext
{
    void RegisterCommand(string commandName, Action<string[]> action); // Registers a command that can be used in the console. The command will only be available if a) no command with the same name already exists and b) no other plugin forcefully overwrites the command.

    void OverwriteCommand(string commandName, Action<string[]> action); // Registers a command with the same name as an existing one, overwriting the old one. Use with caution.
}