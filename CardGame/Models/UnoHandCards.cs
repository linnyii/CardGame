namespace CardGame.Models;

public class UnoHandCards
{
    public List<UnoCard> Cards { get; } = [];

    public IEnumerable<UnoCard> GetPlayableHandCards(UnoCard? currentCard)
    {
        return Cards.Where(card => card.CanPlay(currentCard!));
    }
}