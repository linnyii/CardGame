using CardGame.Games;
using CardGame.Players;
using CardGame.Services;

namespace CardGame;

public static class Program
{
    private const int DefaultPlayerCount = 4;
    private const int UnoGame = 1;
    private const int PokerGame = 2;
    private const int Exit = 3;
    private static readonly IConsoleGameUi UI = new ConsoleConsoleGameUi();
    private static readonly IConsoleInput ConsoleInput = new ConsoleConsoleInput(UI);

    private static void Main()
    {
        UI.DisplayLine("╔════════════════════════════════════╗");
        UI.DisplayLine("║     Welcome to the Card Game       ║");
        UI.DisplayLine("╚════════════════════════════════════╝");

        while (true)
        {
            UI.DisplayEmptyLine();
            UI.DisplayLine("Select a game:");
            UI.DisplayLine("1. UNO Game");
            UI.DisplayLine("2. Poker Game");
            UI.DisplayLine("3. Exit");

            var choice = ConsoleInput.GetMenuChoice("\nEnter your choice (1-3): ", 1, 3);

            switch (choice)
            {
                case UnoGame:
                    PlayUnoGame();
                    break;
                case PokerGame:
                    PlayPokerGame();
                    break;
                case Exit:
                    UI.DisplayLine("\nThank you for playing! Goodbye!");
                    return;
            }
        }
    }

    private static void PlayUnoGame()
    {
        UI.DisplaySection("Creating UNO Game");
        
        var players = CreatePlayers();
        var game = new UnoGame(UI, ConsoleInput, players);
        
        game.StartGame();
    }

    private static void PlayPokerGame()
    {
        UI.DisplaySection("Creating Poker Game");

        var players = CreatePlayers();
        var game = new PokerGame(UI, ConsoleInput, players);
        
        game.StartGame();
    }

    private static List<Player> CreatePlayers()
    {
        var players = new List<Player>();

        for (var playerCount = 0; playerCount < DefaultPlayerCount; playerCount++)
        {
            UI.DisplayEmptyLine();
            UI.DisplayLine($"--- Player {playerCount + 1} ---");
            UI.DisplayLine("1. Human Player");
            UI.DisplayLine("2. AI Player");

            var typeChoice = ConsoleInput.GetMenuChoice("Select player type (1-2): ", 1, 2);

            switch (typeChoice)
            {
                case 1:
                    players.Add(CreateHumanPlayer(playerCount));
                    break;
                case 2:
                    players.Add(CreateAiPlayer(playerCount));
                    break;
                default:
                    UI.DisplayLine("Invalid choice, creating AI player by default.");
                    players.Add(new AiPlayer($"AI{playerCount + 1}"));
                    break;
            }
        }

        return players;
    }

    private static AiPlayer CreateAiPlayer(int playerCount)
    {
        var name = ConsoleInput.GetPlayerName("Enter AI name: ", $"AI{playerCount + 1}");
        return new AiPlayer(name);
    }

    private static HumanPlayer CreateHumanPlayer(int playerCount)
    {
        var name = ConsoleInput.GetPlayerName("Enter player name: ", $"Player{playerCount + 1}");
        return new HumanPlayer(name);
    }
}