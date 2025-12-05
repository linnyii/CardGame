namespace CardGame.ConsoleDisplays;

public interface IConsoleInput
{
    int GetMenuChoice(string prompt, int minValue, int maxValue);
    int GetCardChoice(string prompt, int totalCards);
    string GetPlayerName(string prompt, string defaultName);
}




