using CardGame.Models;
using CardGame.Players;
using CardGame.Services;

namespace CardGame.Games;

public class PokerGame(IConsoleGameUi ui, IConsoleInput consoleInput) : Game(ui, consoleInput)
{
    private readonly PokerDeck _deck = new();
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
            player.SetHandCards(new PokerHandCards());
        }
        
        for (var handCardIndex = 0; handCardIndex < HandCardNumberPerPlayer; handCardIndex++)
        {
            foreach (var player in Players)
            {
                var handCards = player.GetHandCards<PokerHandCards>();
                handCards.Cards.Add(_deck.DrawCard()!);
            }
        }
    }

    public override void PlayRound()
    {
        var playedCardsPerRound = new Dictionary<Player, PokerCard>();
        
        foreach (var player in Players)
        {
            var handCards = player.GetHandCards<PokerHandCards>();
            
            var cardToPlay = player switch
            {
                HumanPlayer => GetHumanPlayerChoice(player, handCards),
                _ => GetAiPlayerChoice(handCards)
            };

            playedCardsPerRound[player] = cardToPlay;
            handCards.Cards.Remove(cardToPlay);
        }

        var roundWinner = DetermineRoundWinner(playedCardsPerRound);
        UI.DisplayRoundWinner(roundWinner.Name);
        
        roundWinner.AddScore();
    }

    private PokerCard GetAiPlayerChoice(PokerHandCards handCards)
    {
        return handCards.RandomChooseCard();
    }

    private PokerCard GetHumanPlayerChoice(Player player, PokerHandCards handCards)
    {
        UI.DisplayEmptyLine();
        UI.DisplayLine($"{player.Name}, please choose a card to play:");
        UI.DisplayPokerHandCards(handCards);
        
        var choice = ConsoleInput.GetCardChoice("Please select a card (enter number): ", handCards.Count);
        
        return handCards.Cards[choice - 1];
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

