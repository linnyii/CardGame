using CardGame.Models;
using CardGame.Players;
using CardGame.ConsoleDisplays;

namespace CardGame.Games;

public class UnoGame(IConsoleGameUi ui, List<Player<UnoCard>> players) 
    : Game<UnoCard>(ui, players)
{
    private readonly UnoDeck _deck = new();
    private UnoCard? _currentCard;
    private const int TotalHandCardPerPlayer = 5;
    private int CurrentPlayerIndex { get; set; }

    
    protected override void DisplayGameStartMessage()
    {
        Ui.DisplayGameStart("UNO Game");
    }

    protected override void InitializeDeck()
    {
        _deck.InitializeDeck();
    }

    protected override void ShuffleDeck()
    {
        _deck.Shuffle();
    }

    protected override void DealCardsToPlayers()
    {
        foreach (var player in Players)
        {
            for (var count = 0; count < TotalHandCardPerPlayer; count++)
            {
                var card = _deck.DrawCard();
                if (card != null)
                {
                    player.Cards.Add(card);
                }
            }
        }
    }

    protected override void AfterDealingCards()
    {
        _currentCard = _deck.DrawCard();
        Ui.DisplayLine($"Starting card: {_currentCard}");
        Ui.DisplayEmptyLine();
    }

    protected override void RunGameLoop()
    {
        while (!IsGameFinished)
        {
            PlayRound();
        }
    }

    protected override void DisplayFinalResults()
    {
        var winner = GetFinalWinner();
        if (winner != null)
        {
            Ui.DisplayWinner(winner.Name);
        }
    }

    public override void PlayRound()
    {
        var currentPlayer = GetCurrentPlayer();
        Ui.DisplayPlayerTurn(currentPlayer.Name);
        Ui.DisplayLine($"Current card: {_currentCard}");
        
        var playableCards = currentPlayer.Cards
            .Where(card => card.CanPlay(_currentCard!))
            .ToList();

        switch (playableCards.Count)
        {
            case > 0:
            {
                currentPlayer.SetSelectableCards(playableCards);
                var cardToPlay = currentPlayer.SelectCard();

                currentPlayer.Cards.Remove(cardToPlay);
                _currentCard = cardToPlay;
                _deck.AddIntoDiscardedCards(cardToPlay);

                if (!IsWinnerComingOut(currentPlayer)) break;
                return;
            }
            default:
            {
                Ui.DisplayLine($"{currentPlayer.Name} has no playable cards, drawing a card...");
                var drawnCard = _deck.DrawCard();
                if (drawnCard == null)
                {
                    _deck.RemoveFromDisCardedCard(_currentCard!);
                    _deck.RePile();
                    currentPlayer.Cards.Add(_deck.DrawCard()!);
                }
                else
                {
                    currentPlayer.Cards.Add(drawnCard);
                }

                Ui.DisplayLine($"Drew: {drawnCard}, added to hand");

                break;
            }
        }
        
        MoveToNextPlayer();
    }

    private bool IsWinnerComingOut(Player<UnoCard> player)
    {
        if (player.HasCards()) return false;
        IsGameFinished = true;
        return true;
    }

    private void MoveToNextPlayer()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
    }

    protected override Player<UnoCard>? GetFinalWinner()
    {
        return Players.FirstOrDefault(player => !player.HasCards());
    }

    private Player<UnoCard> GetCurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }
}

