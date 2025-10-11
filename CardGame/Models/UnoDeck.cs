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
            for (var number = 0; number <= 9; number++)
            {
                Cards.Add(new UnoCard(color, number));
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
        Cards = DisCardedCards;
        Shuffle();
    }
}