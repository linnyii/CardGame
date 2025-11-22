using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public abstract class Game(IConsoleGameUi ui, IConsoleInput consoleInput)
{
    protected List<Player> Players { get; } = [];
    protected bool IsGameFinished { get; set; }
    protected IConsoleGameUi UI { get; } = ui;
    protected IConsoleInput ConsoleInput { get; } = consoleInput;

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }

    public abstract void StartGame();
    
    public abstract void PlayRound();
    
    public abstract Player? GetFinalWinner();
}

