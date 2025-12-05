namespace CardGame.Players;

public abstract class Player<TCard>(string name, int? maxCards = null)
{
    public string Name { get; } = name;
    public int Score { get; private set; }
    public List<TCard> Cards { get; } = [];
    private int? MaxCards { get; } = maxCards;

    public void AddScore()
    {
        Score += 1;
    }
    
    public int CardCount => Cards.Count;
    
    public bool HasCards() => Cards.Count > 0;
    
    public void ReceiveCard(TCard card)
    {
        if (MaxCards.HasValue && Cards.Count >= MaxCards.Value)
        {
            throw new InvalidOperationException($"Player {Name} cannot have more than {MaxCards.Value} cards.");
        }
        Cards.Add(card);
    }

    public abstract TCard SelectCard(List<TCard> availableCards);
}

