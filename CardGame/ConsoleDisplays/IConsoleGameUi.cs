using CardGame.Models;

namespace CardGame.ConsoleDisplays;

public interface IConsoleGameUi
{
    void DisplayMessage(string message);
    void DisplayLine(string message);
    void DisplayEmptyLine();
    void DisplaySection(string section);
    void DisplayPlayerTurn(string playerName);
    void DisplayWinner(string playerName, int score = 0);
    void DisplayGameStart(string gameName);
    void DisplayGameEnd();
    void DisplayRoundNumber(int roundNumber);
    void DisplayRoundWinner(string playerName);
    void DisplayPokerHandCards(List<PokerCard> cards);
}




