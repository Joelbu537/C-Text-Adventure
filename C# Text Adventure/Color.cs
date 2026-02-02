namespace TextAdventure;
public static class Color
{
    public const string RESET = "\x1B[0m";

    public const string FORE_BLACK = "\x1B[38;5;000m";
    public const string FORE_RED = "\x1B[38;5;001m";
    public const string FORE_GREEN = "\x1B[38;5;002m";          // Friendly NPC
    public const string FORE_YELLOW = "\x1B[38;5;003m";         // Unknown/Neutral NPC
    public const string FORE_BLUE = "\x1B[38;5;004m";           
    public const string FORE_PURPLE = "\033[38;5;005";
    public const string FORE_CYAN = "\x1B[38;5;006m";           // Items
    public const string FORE_LIGHT_GREY = "\x1B[38;5;007m";
    public const string FORE_GREY = "\x1B[38;5;008m";
    public const string FORE_LIGHT_RED = "\x1B[38;5;009m";      // Hostile NPC, Error, Death
    public const string FORE_LIGHT_GREEN = "\x1B[38;5;010m";    
    public const string FORE_LIGHT_YELLOW = "\x1B[38;5;011m";
    public const string FORE_LIGHT_BLUE = "\x1B[38;5;012m";
    public const string FORE_LIGHT_PURPLE = "\x1B[38;5;013m";   // Weapon
    public const string FORE_LIGHT_CYAN = "\x1B[38;5;014m";     // Player name
    public const string FORE_WHITE = "\x1B[38;5;015m";          // Points of interest
    public const string FORE_ORANGE = "\x1B[38,5,202m";         // 
    public const string FORE_LIGHT_ORANGE = "\x1B[38;5;208m";   // Armor

    public const string BACK_BLACK = "\x1B[48;5;000m";
    public const string BACK_RED = "\x1B[48;5;001m";
    public const string BACK_GREEN = "\x1B[48;5;002m";
    public const string BACK_YELLOW = "\x1B[48;5;003m";
    public const string BACK_BLUE = "\x1B[48;5;004m";
    public const string BACK_PURPLE = "\x1B[48;5;005";
    public const string BACK_CYAN = "\x1B[48;5;006m";
    public const string BACK_LIGHT_GREY = "\x1B[48;5;007m";
    public const string BACK_GREY = "\x1B[48;5;008m";
    public const string BACK_LIGHT_RED = "\x1B[48;5;009m";
    public const string BACK_LIGHT_GREEN = "\x1B[48;5;010m";
    public const string BACK_LIGHT_YELLOW = "\x1B[48;5;011m";
    public const string BACK_LIGHT_BLUE = "\x1B[48;5;012m";
    public const string BACK_LIGHT_PURPLE = "\x1B[48;5;013m";
    public const string BACK_LIGHT_CYAN = "\x1B[48;5;014m";
    public const string BACK_WHITE = "\x1B[48;5;015m";

    public static readonly List<string> COLOR_LIST = new List<string>
    {
        RESET,
        FORE_BLACK,
        FORE_RED,
        FORE_GREEN,
        FORE_YELLOW,
        FORE_BLUE,
        FORE_PURPLE,
        FORE_CYAN,
        FORE_LIGHT_GREY,
        FORE_GREY,
        FORE_LIGHT_RED,
        FORE_LIGHT_GREEN,
        FORE_LIGHT_YELLOW,
        FORE_LIGHT_BLUE,
        FORE_LIGHT_PURPLE,
        FORE_LIGHT_CYAN,
        FORE_WHITE,
        FORE_ORANGE,
        FORE_LIGHT_ORANGE,  
        BACK_BLACK,
        BACK_RED,
        BACK_GREEN,
        BACK_YELLOW,
        BACK_BLUE,
        BACK_PURPLE,
        BACK_CYAN,
        BACK_LIGHT_GREY,
        BACK_GREY,
        BACK_LIGHT_RED,
        BACK_LIGHT_GREEN,
        BACK_LIGHT_YELLOW,
        BACK_LIGHT_BLUE,
        BACK_LIGHT_PURPLE,
        BACK_LIGHT_CYAN,
        BACK_WHITE
    };
}