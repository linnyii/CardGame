namespace CardGame.Players;

public abstract class Player<TCard>(string name)
{
    public string Name { get; } = name;
    public int Score { get; private set; }
    public List<TCard> Cards { get; } = [];

    public void AddScore()
    {
        Score += 1;
    }
    
    public int CardCount => Cards.Count;
    
    public bool HasCards() => Cards.Count > 0;
}

