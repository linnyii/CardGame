using CardGame.Enums;

namespace CardGame.Models;

public class PokerDeck : Deck<PokerCard>
{
    public PokerDeck()
    {
        InitializeDeck();
    }
    public sealed override void InitializeDeck()
    {
        Cards.Clear();
        
        foreach (var suit in Enum.GetValues<Suit>())
        {
            foreach (var rank in Enum.GetValues<Rank>())
            {
                Cards.Add(new PokerCard(suit, rank));
            }
        }
    }

}

