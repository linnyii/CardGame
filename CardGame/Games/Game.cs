using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public abstract class Game(IConsoleGameUi ui, IConsoleInput consoleInput, List<Player> players)
{
    protected List<Player> Players { get; } = players;
    protected bool IsGameFinished { get; set; }
    protected IConsoleGameUi UI { get; } = ui;
    protected IConsoleInput ConsoleInput { get; } = consoleInput;

    public abstract void StartGame();
    
    public abstract void PlayRound();
    
    public abstract Player? GetFinalWinner();
}

