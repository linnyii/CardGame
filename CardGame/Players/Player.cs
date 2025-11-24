using CardGame.Models;

namespace CardGame.Players;

public abstract class Player(string name)
{
    public string Name { get; } = name;
    public int Score { get; private set; }
    private IHandCards? _handCards;

    public void AddScore()
    {
        Score += 1;
    }
    
    public void SetHandCards(IHandCards handCards)
    {
        _handCards = handCards;
    }
    
    public THandCards GetHandCards<THandCards>() where THandCards : class, IHandCards
    {
        return (_handCards as THandCards)!;
    }
    
    public bool HasCards() => _handCards?.HasCards() ?? false;
}

