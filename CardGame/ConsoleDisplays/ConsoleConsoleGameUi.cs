using CardGame.Models;

namespace CardGame.ConsoleDisplays;

public class ConsoleConsoleGameUi : IConsoleGameUi
{
    public void DisplayMessage(string message)
    {
        Console.Write(message);
    }

    public void DisplayLine(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayEmptyLine()
    {
        Console.WriteLine();
    }

    public void DisplaySection(string section)
    {
        Console.WriteLine($"\n=== {section} ===");
    }

    public void DisplayPlayerTurn(string playerName)
    {
        Console.WriteLine($"\n--- {playerName}'s Turn ---");
    }

    public void DisplayWinner(string playerName, int score = 0)
    {
        if (score > 0)
        {
            Console.WriteLine($"\nWinner: {playerName} (Score: {score})");
        }
        else
        {
            Console.WriteLine($"\n{playerName} wins the game!");
        }
    }

    public void DisplayGameStart(string gameName)
    {
        Console.WriteLine($"\n=== {gameName} Start ===\n");
    }

    public void DisplayGameEnd()
    {
        Console.WriteLine("\nGame Over! Final Results:");
    }

    public void DisplayRoundNumber(int roundNumber)
    {
        Console.WriteLine($"\n========== Round {roundNumber} ==========");
    }

    public void DisplayRoundWinner(string playerName)
    {
        Console.WriteLine($"\nRound Winner: {playerName}");
    }

    public void DisplayPokerHandCards(List<PokerCard> cards)
    {
        var cardIndex = 1;
        foreach (var card in cards)
        {
            Console.WriteLine($"{cardIndex}. {card.Rank} of {card.Suit}");
            cardIndex++;
        }
    }
}

