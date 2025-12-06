using CardGame.Models;
using CardGame.Players;
using CardGame.ConsoleDisplays;

namespace CardGame.Games;

public class PokerGame(IConsoleGameUi ui, List<Player<PokerCard>> players) 
    : Game<PokerCard>(ui, players)
{
    private readonly PokerDeck _deck = new();
    private const int TotalRounds = 13;
    private int _currentRound;
    private const int TotalHandCardsPerPlayer = 13;

    
    protected override void DisplayGameStartMessage()
    {
        Ui.DisplayGameStart("Poker Game");
        Ui.DisplayLine($"Total of {TotalRounds} rounds will be played");
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
        for (var handCardIndex = 0; handCardIndex < TotalHandCardsPerPlayer; handCardIndex++)
        {
            foreach (var player in Players)
            {
                player.ReceiveCard(_deck.DrawCard()!);
            }
        }
    }

    protected override void RunGameLoop()
    {
        while (_currentRound < TotalRounds)
        {
            _currentRound++;
            Ui.DisplayRoundNumber(_currentRound);
            PlayRound();
        }
    }

    public override void PlayRound()
    {
        var playedCardsPerRound = new Dictionary<Player<PokerCard>, PokerCard>();
        
        foreach (var player in Players)
        {
            var cardToPlay = player.SelectCard();

            playedCardsPerRound[player] = cardToPlay;
            player.Cards.Remove(cardToPlay);
        }

        var roundWinner = DetermineRoundWinner(playedCardsPerRound);
        Ui.DisplayRoundWinner(roundWinner.Name);
        
        roundWinner.AddScore();
    }

    private static Player<PokerCard> DetermineRoundWinner(Dictionary<Player<PokerCard>, PokerCard> playedCards)
    {
        Player<PokerCard>? winner = null;
        PokerCard? highestCard = null;

        foreach (var cardPair in playedCards.Where(cardPair => highestCard == null || IsBiggerThanCurrentHighestCard(cardPair, highestCard)))
        {
            highestCard = cardPair.Value;
            winner = cardPair.Key;
        }

        return winner!;
    }

    private static bool IsBiggerThanCurrentHighestCard(KeyValuePair<Player<PokerCard>, PokerCard> playerCardPair, PokerCard highestCard)
    {
        return playerCardPair.Value.CompareTo(highestCard) > 0;
    }

    protected override Player<PokerCard> GetFinalWinner()
    {
        var winner = Players[0];
        foreach (var player in Players.Where(player => player.Score > winner.Score))
        {
            winner = player;
        }
        return winner;
    }
}

