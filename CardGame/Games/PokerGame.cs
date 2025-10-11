using CardGame.Models;
using CardGame.Players;

namespace CardGame.Games;

public class PokerGame : Game
{
    private readonly PokerDeck _deck = new();
    private readonly Dictionary<Player, PokerHandCards> _playerHandCards = new();
    private const int TotalRounds = 13;
    private int _currentRound;
    private const int HandCardNumberPerPlayer = 13;

    public override void StartGame()
    {
        Console.WriteLine("\n=== 撲克遊戲開始 ===");
        Console.WriteLine($"總共進行 {TotalRounds} 輪遊戲\n");
        
        _deck.InitializeDeck();
        _deck.Shuffle();
        
        Console.WriteLine("發牌中...\n");

        DealingCardsToPlayers();
        
        Console.WriteLine("結束發牌\n");
        
        while (_currentRound < TotalRounds && !IsGameFinished)
        {
            _currentRound++;
            Console.WriteLine($"\n========== 第 {_currentRound} 輪 ==========");
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
        Console.WriteLine($"\n本回合贏家: {roundWinner.Name} 🎉");
        
        roundWinner.AddScore();
        
    }

    private PokerCard GetAiPlayerChoice(Player player)
    {
        return _playerHandCards[player].RandomChooseCard();
    }

    private PokerCard GetHumanPlayerChoice(Player player)
    {
        var handCard = _playerHandCards[player];
        Console.WriteLine($"\n{player.Name}，請選擇要打出的牌:");
        
        handCard.DisplayEachCard();

        return handCard.ManualChooseACard();
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
        Console.WriteLine("遊戲結束！最終結果：");
        
        var winner = GetFinalWinner();
        
        Console.WriteLine($"\n總冠軍: {winner.Name} (分數: {winner.Score}) \n");
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

