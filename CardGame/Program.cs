using CardGame.Games;
using CardGame.Players;

namespace CardGame;

public static class Program
{
    private const int DefaultPlayerCount = 4;

    private static void Main()
    {
        Console.WriteLine("╔════════════════════════════════════╗");
        Console.WriteLine("║     Welcome to the Card Game       ║");
        Console.WriteLine("╚════════════════════════════════════╝");

        while (true)
        {
            Console.WriteLine("\n請選擇遊戲:");
            Console.WriteLine("1. Uno 遊戲");
            Console.WriteLine("2. 撲克遊戲");
            Console.WriteLine("3. 退出");
            Console.Write("\n請輸入選項 (1-3): ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    PlayUnoGame();
                    break;
                case "2":
                    PlayPokerGame();
                    break;
                case "3":
                    Console.WriteLine("\n感謝遊玩！再見！");
                    return;
                default:
                    Console.WriteLine("\n無效的選項，請重新選擇。");
                    break;
            }
        }
    }

    private static void PlayUnoGame()
    {
        Console.WriteLine("\n=== 創建 Uno 遊戲 ===");
        
        var players = CreatePlayers();
        var game = new UnoGame();
        
        JoinGame(players, game);
        game.StartGame();
    }

    private static void JoinGame(List<Player> players, Game game)
    {
        foreach (var player in players)
        {
            game.AddPlayer(player);
        }
    }

    private static void PlayPokerGame()
    {
        Console.WriteLine("\n=== 創建撲克遊戲 ===");

        var players = CreatePlayers();
        var game = new PokerGame();
        
        JoinGame(players, game);
        game.StartGame();
    }

    private static List<Player> CreatePlayers()
    {
        var players = new List<Player>();

        for (var playerCount = 0; playerCount < DefaultPlayerCount; playerCount++)
        {
            Console.WriteLine($"\n--- 玩家 {playerCount + 1} ---");
            Console.WriteLine("1. 人類玩家");
            Console.WriteLine("2. AI 玩家");
            Console.Write("請選擇玩家類型 (1-2): ");

            var typeChoice = Console.ReadLine();

            switch (typeChoice)
            {
                case "1":
                {
                    players.Add(CreateHumanPlayer(playerCount));
                    break;
                }
                case "2":
                {
                    players.Add(CreateAiPlayer(playerCount));
                    break;
                }
                default:
                    Console.WriteLine("無效的選擇，默認創建 AI 玩家。");
                    players.Add(new AiPlayer($"AI{playerCount + 1}"));
                    break;
            }
        }

        return players;
    }

    private static AiPlayer CreateAiPlayer(int playerCount)
    {
        Console.Write("請輸入 AI 名稱: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            name = $"AI{playerCount + 1}";
        }

        return new AiPlayer(name);
    }

    private static HumanPlayer CreateHumanPlayer(int playerCount)
    {
        Console.Write("請輸入玩家姓名: ");
        var name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            name = $"玩家{playerCount + 1}";
        }

        return new HumanPlayer(name);
    }
}