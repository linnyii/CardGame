using CardGame.ConsoleDisplays;

namespace CardGame.Players;

public class AiPlayer<TCard>(string name, IConsoleGameUi ui, int? maxCards = null) : Player<TCard>(name, maxCards)
{
    private IConsoleGameUi Ui { get; } = ui;

    protected override TCard ProcessCardSelect()
    {
        var selectableCards = GetSelectableCards();
        var selectedCard = selectableCards[Random.Shared.Next(selectableCards.Count)];
        Ui.DisplayLine($"{Name} plays: {selectedCard}");
        return selectedCard;
    }
}

