using CardGame.Models;
using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public class UnoGame(IConsoleGameUi ui, IConsoleInput consoleInput, List<Player> players) 
    : Game(ui, consoleInput, players)
{
    private readonly UnoDeck _deck = new();
    private UnoCard? _currentCard;
    private const int TotalHandCardPerPlayer = 5;
    private int CurrentPlayerIndex { get; set; }

    public override void StartGame()
    {
        UI.DisplayGameStart("UNO Game");
        
        _deck.InitializeDeck();
        _deck.Shuffle();
        DealingCardToPlayers();

        _currentCard = _deck.DrawCard();
        UI.DisplayLine($"Starting card: {_currentCard}");
        UI.DisplayEmptyLine();

        while (!IsGameFinished)
        {
            PlayRound();
        }

        var winner = GetFinalWinner();
        UI.DisplayWinner(winner!.Name);
    }

    private void DealingCardToPlayers()
    {
        foreach (var player in Players)
        {
            var handCards = new UnoHandCards();
            for (var count = 0; count < TotalHandCardPerPlayer; count++)
            {
                var card = _deck.DrawCard();
                if (card != null)
                {
                    handCards.AddHandCard(card);
                }
            }
            player.SetHandCards(handCards);
        }
    }

    public override void PlayRound()
    {
        var currentPlayer = GetCurrentPlayer();
        UI.DisplayPlayerTurn(currentPlayer.Name);
        UI.DisplayLine($"Current card: {_currentCard}");
        
        var handCards = currentPlayer.GetHandCards<UnoHandCards>();
        var playableCards = handCards.GetPlayableHandCards(_currentCard).ToList();

        switch (playableCards.Count)
        {
            case > 0:
            {
                var cardToPlay = currentPlayer switch
                {
                    HumanPlayer => GetHumanPlayerChoice(playableCards),
                    _ => GetAiPlayerChoice(playableCards, currentPlayer)
                };

                handCards.Cards.Remove(cardToPlay);
                _currentCard = cardToPlay;
                _deck.AddIntoDiscardedCards(cardToPlay);

                if (!IsWinnerComingOut(currentPlayer)) break;
                return;
            }
            default:
            {
                UI.DisplayLine($"{currentPlayer.Name} has no playable cards, drawing a card...");
                var drawnCard = _deck.DrawCard();
                if (drawnCard == null)
                {
                    _deck.RemoveFromDisCardedCard(_currentCard!);
                    _deck.RePile();
                    handCards.Cards.Add(_deck.DrawCard()!);
                }
                else
                {
                    handCards.Cards.Add(drawnCard);
                }

                UI.DisplayLine($"Drew: {drawnCard}, added to hand");

                break;
            }
        }
        
        MoveToNextPlayer();
    }

    private bool IsWinnerComingOut(Player player)
    {
        if (player.HasCards()) return false;
        IsGameFinished = true;
        return true;
    }

    private UnoCard GetAiPlayerChoice(List<UnoCard> playableCards, Player currentPlayer)
    {
        var cardToPlay = playableCards[Random.Shared.Next(playableCards.Count)];
        UI.DisplayLine($"{currentPlayer.Name} plays: {cardToPlay}");
        return cardToPlay;
    }

    private UnoCard GetHumanPlayerChoice(List<UnoCard> playableCards)
    {
        UI.DisplayEmptyLine();
        UI.DisplayLine("Playable cards:");
        for (var i = 0; i < playableCards.Count; i++)
        {
            UI.DisplayLine($"{i + 1}. {playableCards[i]}");
        }

        var choice = ConsoleInput.GetCardChoice("Select a card to play (enter number): ", playableCards.Count);
        return playableCards[choice - 1];
    }

    private void MoveToNextPlayer()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
    }

    public override Player? GetFinalWinner()
    {
        return Players.FirstOrDefault(player => !player.HasCards());
    }

    private Player GetCurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }
}

