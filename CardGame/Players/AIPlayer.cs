using CardGame.ConsoleDisplays;

namespace CardGame.Players;

public class AiPlayer<TCard>(string name, IConsoleGameUi ui, int? maxCards = null) : Player<TCard>(name, maxCards)
{
    private IConsoleGameUi Ui { get; } = ui;

    public override TCard SelectCard(List<TCard> availableCards)
    {
        var selectedCard = availableCards[Random.Shared.Next(availableCards.Count)];
        Ui.DisplayLine($"{Name} plays: {selectedCard}");
        return selectedCard;
    }
}

