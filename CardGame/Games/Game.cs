using CardGame.Players;
using CardGame.ConsoleDisplays;

namespace CardGame.Games;

public abstract class Game<TCard>(IConsoleGameUi ui, List<Player<TCard>> players)
{
    protected List<Player<TCard>> Players { get; } = players;
    protected bool IsGameFinished { get; set; }
    protected IConsoleGameUi Ui { get; } = ui;

    public void StartGame()
    {
        Initialize();
        RunGameLoop();
        Finish();
    }

    private void Initialize()
    {
        DisplayGameStartMessage();
        InitializeDeck();
        ShuffleDeck();
        Ui.DisplayLine("Dealing cards...");
        DealCardsToPlayers();
        Ui.DisplayLine("Finished dealing cards");
        AfterDealingCards();
    }

    private void Finish()
    {
        DisplayFinalResults();
    }
    
    protected abstract void DisplayGameStartMessage();
    protected abstract void InitializeDeck();
    protected abstract void ShuffleDeck();
    protected abstract void DealCardsToPlayers();
    protected abstract void RunGameLoop();
    public abstract void PlayRound();
    protected abstract Player<TCard>? GetFinalWinner();
    
    protected virtual void AfterDealingCards()
    {
    }
    
    protected virtual void DisplayFinalResults()
    {
        Ui.DisplayGameEnd();
        var winner = GetFinalWinner();
        if (winner != null)
        {
            Ui.DisplayWinner(winner.Name, winner.Score);
        }
    }
}

