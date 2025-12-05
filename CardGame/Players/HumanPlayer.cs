using CardGame.ConsoleDisplays;

namespace CardGame.Players;

public class HumanPlayer<TCard>(string name, IConsoleGameUi ui, IConsoleInput consoleInput, int? maxCards = null) 
    : Player<TCard>(name, maxCards)
{
    private IConsoleGameUi Ui { get; } = ui;
    private IConsoleInput ConsoleInput { get; } = consoleInput;

    public override TCard SelectCard(List<TCard> availableCards)
    {
        Ui.DisplayLine($"{Name}, please choose a card to play:");
        
        for (var i = 0; i < availableCards.Count; i++)
        {
            Ui.DisplayLine($"{i + 1}. {availableCards[i]}");
        }

        var choice = ConsoleInput.GetCardChoice("Select a card to play (enter number): ", availableCards.Count);
        
        return availableCards[choice - 1];
    }
}

