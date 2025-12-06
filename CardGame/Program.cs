using CardGame.Games;
using CardGame.Models;
using CardGame.Players;
using CardGame.ConsoleDisplays;

namespace CardGame;

public static class Program
{
    private const int DefaultPlayerCount = 4;
    private const int UnoGame = 1;
    private const int PokerGame = 2;
    private const int Exit = 3;
    private static readonly IConsoleGameUi Ui = new ConsoleGameUi();
    private static readonly IConsoleInput ConsoleInput = new ConsoleInput(Ui);

    private static void Main()
    {
        Ui.DisplayLine("╔════════════════════════════════════╗");
        Ui.DisplayLine("║     Welcome to the Card Game       ║");
        Ui.DisplayLine("╚════════════════════════════════════╝");

        while (true)
        {
            Ui.DisplayEmptyLine();
            Ui.DisplayLine("Select a game:");
            Ui.DisplayLine("1. UNO Game");
            Ui.DisplayLine("2. Poker Game");
            Ui.DisplayLine("3. Exit");

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
                    Ui.DisplayLine("\nThank you for playing! Goodbye!");
                    return;
            }
        }
    }

    private static void PlayUnoGame()
    {
        Ui.DisplaySection("Creating UNO Game");
        
        var players = CreatePlayers<UnoCard>();
        var game = new UnoGame(Ui, players);
        
        game.StartGame();
    }

    private static void PlayPokerGame()
    {
        Ui.DisplaySection("Creating Poker Game");

        var players = CreatePlayers<PokerCard>(maxCards: 13);
        var game = new PokerGame(Ui, players);
        
        game.StartGame();
    }

    private static List<Player<TCard>> CreatePlayers<TCard>(int? maxCards = null)
    {
        var players = new List<Player<TCard>>();

        for (var playerCount = 0; playerCount < DefaultPlayerCount; playerCount++)
        {
            Ui.DisplayEmptyLine();
            Ui.DisplayLine($"--- Player {playerCount + 1} ---");
            Ui.DisplayLine("1. Human Player");
            Ui.DisplayLine("2. AI Player");

            var typeChoice = ConsoleInput.GetMenuChoice("Select player type (1-2): ", 1, 2);

            switch (typeChoice)
            {
                case 1:
                    players.Add(CreateHumanPlayer<TCard>(playerCount, maxCards));
                    break;
                case 2:
                    players.Add(CreateAiPlayer<TCard>(playerCount, maxCards));
                    break;
                default:
                    Ui.DisplayLine("Invalid choice, creating AI player by default.");
                    players.Add(new AiPlayer<TCard>($"AI{playerCount + 1}", Ui, maxCards));
                    break;
            }
        }

        return players;
    }

    private static AiPlayer<TCard> CreateAiPlayer<TCard>(int playerCount, int? maxCards = null)
    {
        var name = ConsoleInput.GetPlayerName("Enter AI name: ", $"AI{playerCount + 1}");
        return new AiPlayer<TCard>(name, Ui, maxCards);
    }

    private static HumanPlayer<TCard> CreateHumanPlayer<TCard>(int playerCount, int? maxCards = null)
    {
        var name = ConsoleInput.GetPlayerName("Enter player name: ", $"Player{playerCount + 1}");
        return new HumanPlayer<TCard>(name, Ui, ConsoleInput, maxCards);
    }
}