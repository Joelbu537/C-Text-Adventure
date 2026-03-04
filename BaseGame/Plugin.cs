namespace BaseGame;

using TextAdventure.PluginContract;
public class BaseGame : IPlugin
{
    public string Name => "Base Game";
    public string Author => "Joelbu";
    public Version Version => new(0, 0, 0);
    public string SourceRepo => "https://github.com/ShitHub-Dev-Team/Text-Adventure";
    public bool Enabled { get; set; }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Initialize(IHostContext host)
    {
        throw new NotImplementedException();
    }
}