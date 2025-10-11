namespace CardGame.Players;

public abstract class Player(string name)
{
    public string Name { get; } = name;
    public int Score { get; private set; }

    public void AddScore()
    {
        Score += 1;
    }
    
}

