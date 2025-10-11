using CardGame.Players;

namespace CardGame.Games;

public abstract class Game
{
    protected List<Player> Players { get; } = [];
    protected bool IsGameFinished { get; set; }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }

    public abstract void StartGame();
    
    public abstract void PlayRound();
    
    public abstract Player? GetFinalWinner();
}

