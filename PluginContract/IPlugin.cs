namespace TextAdventure.PluginContract;
public interface IPlugin
{
    public string Name { get; }
    public string Author { get; }
    public string SourceRepo { get; }
    public Version Version { get; }
    public bool Enabled { get; set; }
    void Initialize(IHostContext host);
    void Execute();
}