using CardGame.Models;
using CardGame.Players;

namespace CardGame.Games;

public class UnoGame : Game
{
    private readonly UnoDeck _deck = new();
    private readonly Dictionary<Player, UnoHandCards> _playerAndHandCards = new();
    private UnoCard? _currentCard;
    private const int HandCardNumberPerPlayer = 5;
    private int CurrentPlayerIndex { get; set; }

    public override void StartGame()
    {
        Console.WriteLine("\n=== Uno 遊戲開始 ===\n");
        
        _deck.InitializeDeck();
        _deck.Shuffle();
        DealingCardToPlayers();

        _currentCard = _deck.DrawCard();
        Console.WriteLine($"起始牌: {_currentCard}\n");

        while (!IsGameFinished)
        {
            PlayRound();
        }

        var winner = GetFinalWinner();
        if (winner != null)
        {
            Console.WriteLine($"\n {winner.Name} 贏得了遊戲！");
        }
    }

    private void DealingCardToPlayers()
    {
        foreach (var player in Players)
        {
            _playerAndHandCards[player] = new UnoHandCards();
            for (var i = 0; i < HandCardNumberPerPlayer; i++)
            {
                var card = _deck.DrawCard();
                if (card != null)
                {
                    _playerAndHandCards[player].Cards.Add(card);
                }
            }
        }
    }

    public override void PlayRound()
    {
        var currentPlayer = GetCurrentPlayer();
        Console.WriteLine($"\n--- {currentPlayer.Name} 的回合 ---");
        Console.WriteLine($"當前牌: {_currentCard}");
        
        var handCards = _playerAndHandCards[currentPlayer];
        Console.WriteLine($"手牌: {string.Join(", ", handCards)}");

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

                if (IsWinnerComingOut(handCards)) return;
                break;
            }
            default:
            {
                Console.WriteLine($"{currentPlayer.Name} 沒有可打的牌，抽一張牌");
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
                    Console.WriteLine($"抽到: {drawnCard}, 並加入手牌");
                }

                break;
            }
        }
        
            MoveToNextPlayer();
    }

    private bool IsWinnerComingOut(UnoHandCards handCards)
    {
        if (handCards.Cards.Count != 0) return false;
        IsGameFinished = true;
        return true;

    }

    private static UnoCard GetAiPlayerChoice(List<UnoCard> playableCards, Player currentPlayer)
    {
        var cardToPlay = playableCards[new Random().Next(playableCards.Count)];
        Console.WriteLine($"{currentPlayer.Name} 打出: {cardToPlay}");
        return cardToPlay;
    }

    private static UnoCard GetHumanPlayerChoice(List<UnoCard> playableCards)
    {
        Console.WriteLine("\n可以打出的牌:");
        for (var i = 0; i < playableCards.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {playableCards[i]}");
        }

        while (true)
        {
            Console.Write("請選擇要打出的牌 (輸入編號): ");
            if (int.TryParse(Console.ReadLine(), out int choice) && 
                choice >= 1 && choice <= playableCards.Count)
            {
                return playableCards[choice - 1];
            }
            Console.WriteLine("無效的選擇，請重新輸入。");
        }
    }

    private void MoveToNextPlayer()
    {
        CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count;
    }

    public override Player? GetFinalWinner()
    {
        return Players.FirstOrDefault(player => _playerAndHandCards[player].Cards.Count == 0);
    }

    private Player GetCurrentPlayer()
    {
        return Players[CurrentPlayerIndex];
    }
}

