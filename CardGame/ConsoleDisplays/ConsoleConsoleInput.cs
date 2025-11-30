namespace CardGame.ConsoleDisplays;

public class ConsoleConsoleInput(IConsoleGameUi ui) : IConsoleInput
{
    public int GetMenuChoice(string prompt, int minValue, int maxValue)
    {
        while (true)
        {
            ui.DisplayMessage(prompt);
            if (int.TryParse(Console.ReadLine(), out var choice) && 
                choice >= minValue && choice <= maxValue)
            {
                return choice;
            }
            ui.DisplayLine($"Invalid choice. Please enter a number between {minValue} and {maxValue}.");
        }
    }

    public int GetCardChoice(string prompt, int totalCards)
    {
        while (true)
        {
            ui.DisplayMessage(prompt);
            if (int.TryParse(Console.ReadLine(), out var choice) && 
                choice >= 1 && choice <= totalCards)
            {
                return choice;
            }
            ui.DisplayLine("Invalid choice. Please try again.");
        }
    }

    public string GetPlayerName(string prompt, string defaultName)
    {
        ui.DisplayMessage(prompt);
        var name = Console.ReadLine();
        return string.IsNullOrWhiteSpace(name) ? defaultName : name;
    }
}

