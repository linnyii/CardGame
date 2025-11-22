using CardGame.Services;

namespace CardGame.Models;

public class PokerHandCards
{
    private static readonly Random SharedRandom = new();
    public List<PokerCard> Cards { get; } = [];

    public void DisplayEachCard(IConsoleGameUi ui)
    {
        var cardIndex = 1;
        foreach (var card in Cards)
        {
            ui.DisplayLine($"{cardIndex}. {card.Rank} of {card.Suit}");
            cardIndex++;
        }
    }

    public PokerCard ManualChooseACard(IConsoleInput consoleInput)
    {
        var choice = consoleInput.GetCardChoice("Please select a card (enter number): ", Cards.Count);
        return Cards[choice - 1];
    }

    public PokerCard RandomChooseCard()
    {
        return Cards[SharedRandom.Next(Cards.Count)];
    }
}