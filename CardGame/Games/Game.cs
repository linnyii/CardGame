using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public abstract class Game<TCard>(IConsoleGameUi ui, IConsoleInput consoleInput, List<Player<TCard>> players)
{
    protected List<Player<TCard>> Players { get; } = players;
    protected bool IsGameFinished { get; set; }
    protected IConsoleGameUi UI { get; } = ui;
    protected IConsoleInput ConsoleInput { get; } = consoleInput;

    public abstract void StartGame();
    
    public abstract void PlayRound();
    
    public abstract Player<TCard>? GetFinalWinner();
}

