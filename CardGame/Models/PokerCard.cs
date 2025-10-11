using CardGame.Enums;

namespace CardGame.Models;

public class PokerCard(Suit suit, Rank rank)
{
    public Suit Suit { get; } = suit;
    public Rank Rank { get; } = rank;

    private int GetPoint()
    {
        return (int)Rank;
    }

    public int CompareTo(PokerCard? currenHighestCard)
    {
        return GetPoint().CompareTo(currenHighestCard!.GetPoint());
    }
    
}

