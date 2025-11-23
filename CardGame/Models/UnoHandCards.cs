namespace CardGame.Models;

public class UnoHandCards : IHandCards
{
    public List<UnoCard> Cards { get; } = [];
    
    public int Count => Cards.Count;
    public bool HasCards() => Cards.Count > 0;

    public IEnumerable<UnoCard> GetPlayableHandCards(UnoCard? currentCard)
    {
        return Cards.Where(card => card.CanPlay(currentCard!));
    }

    public void AddHandCard(UnoCard card)
    {
        Cards.Add(card);
    }
}