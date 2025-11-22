using CardGame.Models;
using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public class PokerGame(IConsoleGameUi ui, IConsoleInput consoleInput) : Game(ui, consoleInput)
{
    private readonly PokerDeck _deck = new();
    private readonly Dictionary<Player, PokerHandCards> _playerHandCards = new();
    private const int TotalRounds = 13;
    private int _currentRound;
    private const int HandCardNumberPerPlayer = 13;

    public override void StartGame()
    {
        UI.DisplayGameStart("Poker Game");
        UI.DisplayLine($"Total of {TotalRounds} rounds will be played");
        UI.DisplayEmptyLine();
        
        _deck.InitializeDeck();
        _deck.Shuffle();
        
        UI.DisplayLine("Dealing cards...");
        UI.DisplayEmptyLine();

        DealingCardsToPlayers();
        
        UI.DisplayLine("Finished dealing cards");
        UI.DisplayEmptyLine();
        
        while (_currentRound < TotalRounds)
        {
            _currentRound++;
            UI.DisplayRoundNumber(_currentRound);
            PlayRound();
        }

        DisplayFinalResults();
    }

    private void DealingCardsToPlayers()
    {
        foreach (var player in Players)
        {
            _playerHandCards[player] = new PokerHandCards();
        }
        
        for (var handCardIndex = 0; handCardIndex < HandCardNumberPerPlayer; handCardIndex++)
        {
            foreach (var player in Players)
            {
                _playerHandCards[player].Cards.Add(_deck.DrawCard()!);
            }
        }
    }

    public override void PlayRound()
    {
        var playedCardsPerRound = new Dictionary<Player, PokerCard>();
        
        foreach (var player in Players)
        {
            var cardToPlay = player switch
            {
                HumanPlayer => GetHumanPlayerChoice(player),
                _ => GetAiPlayerChoice(player)
            };

            playedCardsPerRound[player] = cardToPlay;
            _playerHandCards[player].Cards.Remove(cardToPlay);
        }

        var roundWinner = DetermineRoundWinner(playedCardsPerRound);
        UI.DisplayRoundWinner(roundWinner.Name);
        
        roundWinner.AddScore();
    }

    private PokerCard GetAiPlayerChoice(Player player)
    {
        return _playerHandCards[player].RandomChooseCard();
    }

    private PokerCard GetHumanPlayerChoice(Player player)
    {
        var handCard = _playerHandCards[player];
        UI.DisplayEmptyLine();
        UI.DisplayLine($"{player.Name}, please choose a card to play:");
        
        handCard.DisplayEachCard(UI);

        return handCard.ManualChooseACard(ConsoleInput);
    }

    private static Player DetermineRoundWinner(Dictionary<Player, PokerCard> playedCards)
    {
        Player? winner = null;
        PokerCard? highestCard = null;

        foreach (var cardPair in playedCards.Where(cardPair => highestCard == null || IsBiggerThanCurrentHighestCard(cardPair, highestCard)))
        {
            highestCard = cardPair.Value;
            winner = cardPair.Key;
        }

        return winner!;
    }

    private static bool IsBiggerThanCurrentHighestCard(KeyValuePair<Player, PokerCard> playerCardPair, PokerCard highestCard)
    {
        return playerCardPair.Value.CompareTo(highestCard) > 0;
    }

    private void DisplayFinalResults()
    {
        UI.DisplayGameEnd();
        
        var winner = GetFinalWinner();
        
        UI.DisplayWinner(winner.Name, winner.Score);
        UI.DisplayEmptyLine();
    }

    public override Player GetFinalWinner()
    {
        
        var winner = Players[0];
        foreach (var player in Players.Where(player => player.Score > winner.Score))
        {
            winner = player;
        }
        return winner;
    }
}

