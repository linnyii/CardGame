using CardGame.Enums;

namespace CardGame.Models;

public class UnoDeck : Deck<UnoCard>
{
    private List<UnoCard> DisCardedCards { get; set; }

    public sealed override void InitializeDeck()
    {
        Cards.Clear();

        CreateUnoCards();
    }


    public UnoDeck()
    {
        DisCardedCards = [];
        InitializeDeck();
    }

    private void CreateUnoCards()
    {
        foreach (var color in Enum.GetValues<Color>())
        {
            foreach (var value in Enum.GetValues<UnoCardValue>())
            {
                Cards.Add(new UnoCard(color, value));
            }
        }
    }

    public void AddIntoDiscardedCards(UnoCard playableCards)
    {
        DisCardedCards.Add(playableCards);
    }

    public void RemoveFromDisCardedCard(UnoCard currentCard)
    {
        DisCardedCards.Remove(currentCard);
    }

    public void RePile()
    {
        Cards = new List<UnoCard>(DisCardedCards);
        DisCardedCards.Clear();
        Shuffle();
    }
}