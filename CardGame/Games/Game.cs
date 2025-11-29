using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public abstract class Game<TCard>(IConsoleGameUi ui, IConsoleInput consoleInput, List<Player<TCard>> players)
{
    protected List<Player<TCard>> Players { get; } = players;
    protected bool IsGameFinished { get; set; }
    protected IConsoleGameUi UI { get; } = ui;
    protected IConsoleInput ConsoleInput { get; } = consoleInput;

    public void StartGame()
    {
        DisplayGameStartMessage();
        InitializeGame();
        PreActionBeforePlayRounds();
        RunGameLoop();
        DisplayFinalResults();
    }
    
    protected abstract void DisplayGameStartMessage();
    protected abstract void InitializeDeck();
    protected abstract void ShuffleDeck();
    protected abstract void DealCardsToPlayers();
    protected abstract void RunGameLoop();
    public abstract void PlayRound();
    protected abstract Player<TCard>? GetFinalWinner();
    
    private void InitializeGame()
    {
        InitializeDeck();
        ShuffleDeck();
        UI.DisplayLine("Dealing cards...");
        DealCardsToPlayers();
        UI.DisplayLine("Finished dealing cards");
    }

    protected virtual void PreActionBeforePlayRounds()
    {
    }
    
    protected virtual void DisplayFinalResults()
    {
        UI.DisplayGameEnd();
        var winner = GetFinalWinner();
        if (winner != null)
        {
            UI.DisplayWinner(winner.Name, winner.Score);
        }
    }
}

