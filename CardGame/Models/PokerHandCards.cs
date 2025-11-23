namespace CardGame.Models;

public class PokerHandCards : IHandCards
{
    private static readonly Random SharedRandom = new();
    public List<PokerCard> Cards { get; } = [];
    
    public int Count => Cards.Count;
    public bool HasCards() => Cards.Count > 0;

    public PokerCard RandomChooseCard()
    {
        return Cards[SharedRandom.Next(Cards.Count)];
    }
}