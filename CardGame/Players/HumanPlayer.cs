using CardGame.ConsoleDisplays;

namespace CardGame.Players;

public class HumanPlayer<TCard>(string name, IConsoleGameUi ui, IConsoleInput consoleInput, int? maxCards = null) 
    : Player<TCard>(name, maxCards)
{
    private IConsoleGameUi Ui { get; } = ui;
    private IConsoleInput ConsoleInput { get; } = consoleInput;

    protected override TCard ProcessCardSelect()
    {
        var selectableCards = GetSelectableCards();
        
        Ui.DisplayLine($"{Name}, please choose a card to play:");
        
        for (var i = 0; i < selectableCards.Count; i++)
        {
            Ui.DisplayLine($"{i + 1}. {selectableCards[i]}");
        }

        var choice = ConsoleInput.GetCardChoice("Select a card to play (enter number): ", selectableCards.Count);
        
        return selectableCards[choice - 1];
    }
}

